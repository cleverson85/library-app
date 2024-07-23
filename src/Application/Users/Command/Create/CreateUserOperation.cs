using Domain.Core.Contract;
using Domain.Core.Operation;
using Application.Extensions;
using Application.Users.Exceptions;
using Application.Users.Validators.Users;
using Domain.Abstraction;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Domain.Abstraction.Repositories;

namespace Application.Users.Command.Create;

public sealed class CreateUserOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<CreateUserRequest, CreateUserResponse>> logger)
    : CoreOperationAsync<CreateUserRequest, CreateUserResponse>(unitOfWork, logger), ICreateUserOperation
{
    protected override async Task<CreateUserResponse> ProcessOperationAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        request.Password = request.Password.HashPassWord();

        var result = await _unitOfWork.GetRepository<IUserRepository>().SaveAsync((User)request, cancellationToken);
        result.Password = string.Empty;

        return (CreateUserResponse)result;
    }

    protected override async Task<ValidationResult> ValidateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await IsUserNameAlreadyExists(request.UserName, cancellationToken);

        if (!result.IsValid)
        {
            return result;
        }

        var validator = new UserCreateValidator();
        return await validator.ValidateAsync((User)request, cancellationToken);
    }

    private async Task<ValidationResult> IsUserNameAlreadyExists(string userName, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.GetRepository<IUserRepository>().GetByUserName(userName, cancellationToken);
        if (user is not null)
        {
            return new UserAlreadyExistsException(userName);
        }

        return new CoreOperationResponse();
    }
}
