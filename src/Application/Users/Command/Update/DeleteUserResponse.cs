using Application.Core.Contract;
using Domain.Entities;

namespace Application.Users.Command.Update;

public sealed class DeleteUserResponse : CoreOperationResponse
{
    public User? User { get; set; }

    public static explicit operator DeleteUserResponse(User user) => new()
    {
        User = user,
    };
}