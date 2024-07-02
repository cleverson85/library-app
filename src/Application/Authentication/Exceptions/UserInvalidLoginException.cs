using Application.Exceptions.Base;

namespace Application.Authentication.Exceptions;

public class UserInvalidLoginException : NotFoundException
{
    public UserInvalidLoginException(string userName)
        : base("User.InvalidLogin", $"The user name {userName} has login attempt invalid.")
    { }
}
