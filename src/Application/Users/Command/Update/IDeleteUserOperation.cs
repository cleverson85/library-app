using Domain.Core.Abstraction;

namespace Application.Users.Command.Update;

public interface IDeleteUserOperation : ICoreOperationAsync<DeleteUserRequest, DeleteUserResponse>
{ }
