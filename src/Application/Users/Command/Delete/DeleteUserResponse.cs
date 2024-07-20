using Domain.Core.Contract;
using Domain.Entities;

namespace Application.Users.Command.Delete;

public sealed class DeleteUserResponse : CoreOperationResponse
{
    public User? User { get; set; }

    public static explicit operator DeleteUserResponse(User user) => new()
    {
        User = user,
    };
}