using Domain.Core.Contract;

namespace Application.Authentication.Command;

public sealed class UserRequest(string userName, string passWord) : CoreOperationRequest
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = passWord;
}
