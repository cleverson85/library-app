using Domain.Core.Abstraction;

namespace Application.Users.Command.Delete;

public interface IDeleteUserOperation : ICoreOperationAsync<DeleteUserRequest, DeleteUserResponse>
{ }
