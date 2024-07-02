using Application.Exceptions.Base;

namespace Application.Users.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userName)
        : base("User.NotFound", $"The user name {userName} was not found.")
    { }
}
