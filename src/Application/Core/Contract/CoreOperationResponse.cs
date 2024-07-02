using FluentValidation.Results;

namespace Application.Core.Contract;

public class CoreOperationResponse : ValidationResult
{
    public void AddError(string code, string message)
    {
        Errors.Add(new ValidationFailure(code, message));
    }
}
