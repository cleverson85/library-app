using Domain.Core.Contract;
using Domain.Entities;

namespace Application.Users.Command.Create;

public sealed class CreateUserRequest : CoreOperationRequest
{
    public CreateUserRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public static explicit operator User(CreateUserRequest userRequest) => new User(userRequest.UserName, userRequest.Password);
}
