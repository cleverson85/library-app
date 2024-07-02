using Domain.Entities;
using FluentValidation;

namespace Application.Users.Validators.Users;

public class UserValidator : AbstractValidator<User>
{
    protected void ValidateUserName()
    {
        RuleFor(c => c.UserName)
               .NotEmpty()
               .Length(2, 20)
               .WithMessage("The user name must have between 2 and 20 characters");
    }

    protected void ValidatePassWord()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .Length(2, 20)
            .WithMessage("Please ensure you have entered a valid password.");
    }
}
