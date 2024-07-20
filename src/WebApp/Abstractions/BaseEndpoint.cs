using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints.Abstractions;

namespace WebApp.Abstractions;

public abstract class BaseEndpoint<T> where T : IEndpoint
{
    protected IResult CustomResponse(ValidationResult result)
    {
        if (IsOperationValid(result))
        {
            TypedResults.Ok(result);
        }

        return TypedResults.BadRequest<ValidationProblemDetails>(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", GetErrorMessages(result) }
        }));
    }

    private static string[] GetErrorMessages(ValidationResult result)
    {
        return result.Errors
                     .Select(c => c.ErrorMessage)
                     .ToArray();
    }

    private static bool IsOperationValid(ValidationResult result)
    {
        return !result.Errors.Any();
    }
}
