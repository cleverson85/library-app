using Domain.Core.Contract;
using Domain.Entities;

namespace Application.Users.Command.Create;

public sealed class CreateUserResponse : CoreOperationResponse
{
    public User? User { get; set; }

    public static explicit operator CreateUserResponse(User user) => new()
    {
        User = user,
    };
}
