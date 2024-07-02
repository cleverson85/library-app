using Application.Core.Abstraction;

namespace Application.Users.Command.Create;

public interface ICreateUserOperation : ICoreOperationAsync<CreateUserRequest, CreateUserResponse>
{ }
