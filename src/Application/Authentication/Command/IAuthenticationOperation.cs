using Application.Core.Abstraction;

namespace Application.Authentication.Command;

public interface IAuthenticationOperation : ICoreOperationAsync<UserRequest, UserResponse>
{ }
