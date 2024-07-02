using Application.Exceptions.Base;

namespace Application.Users.Exceptions;

public class UserAlreadyExistsException : BadRequestException
{
    public UserAlreadyExistsException(string userName) 
        : base("User.AlreadyExists", $"The user with username {userName} already exists.")
    { }
}