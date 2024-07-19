using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints.Abstractions;

namespace WebApp.Abstractions;

public abstract class BaseEndpoint<T> where T : IEndpoint
{
    private readonly ICollection<string> _errors = new List<string>();

    protected IResult CustomResponse(object? result = null)
    {
        if (IsOperationValid())
        {
            return Results.Ok(result);
        }

        return Results.BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", _errors.ToArray() }
        }));
    }

    protected IResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddError(error.ErrorMessage);
        }

        return CustomResponse(result: validationResult);
    }

    protected bool IsOperationValid()
    {
        return !_errors.Any();
    }

    protected void AddError(string erro)
    {
        _errors.Add(erro);
    }
}
