using Domain.Entities;
using FluentValidation;

namespace Application.Books.Validators.Books;

public class BookValidator : AbstractValidator<Book>
{
    protected void ValidateAuthor()
    {
        RuleFor(c => c.Author)
               .NotEmpty()
               .Length(2, 100)
               .WithMessage("The Author Name must have between 2 and 100 characters.");
    }

    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Please ensure you have entered the Title.");
    }

    protected void ValidateRegisterNumber()
    {
        RuleFor(c => c.RegisterNumber)
            .NotEmpty()
            .Length(4, 10)
            .WithMessage("Book register must have between 4 and 10 characters.");
    }
}
