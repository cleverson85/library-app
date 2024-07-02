using Application.Exceptions.Base;

namespace Application.Books.Exceptions;

public class BookAlreadyExistisdException : BadRequestException
{
    public BookAlreadyExistisdException(string registerNumber)
        : base("Book.AlreadyExists", $"The book with register number {registerNumber} already exists.")
    { }
}
