using Application.Core.Contract;

namespace Application.Authentication.Command;

public sealed class UserResponse : CoreOperationResponse
{
    public bool IsAuthenticaded { get; set; }
    public string Token { get; set; } = string.Empty;
}
