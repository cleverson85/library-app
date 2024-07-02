using Application.Core.Operation;
using Domain.Abstraction;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Users.Command.Update;

public sealed class DeleteUserOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<DeleteUserRequest, DeleteUserResponse>> logger)
    : CoreOperationAsync<DeleteUserRequest, DeleteUserResponse>(unitOfWork, logger), IDeleteUserOperation
{
    protected override async Task<DeleteUserResponse> ProcessOperationAsync(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.UserRepository.DeleteAsync(request.Id);
        if (result <= 0)
        {
            var response = new DeleteUserResponse();
            response.AddError("User.NotFound", $"The user name {request.UserName} was not found.");
            return response;
        }

        return new DeleteUserResponse();
    }
}
