namespace Application.Users.Validators.Users;

public class UserCreateValidator : UserValidator
{
    public UserCreateValidator()
    {
        ValidateUserName();
        ValidatePassWord();
    }
}
