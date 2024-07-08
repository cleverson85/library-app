using Domain.Core.Contract;
using Domain.Entities;

namespace Application.Users.Command.Update;

public sealed class DeleteUserRequest(string Id) : CoreOperationRequest
{
    public string Id { get; set; } = Id;
    public string UserName { get; set; } = string.Empty;

    public static explicit operator User(DeleteUserRequest userRequest) => new()
    {
        UserName = userRequest.UserName,
        Id = userRequest.Id
    };
}
