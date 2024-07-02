using Application.Exceptions.Base;

namespace Application.Books.Exceptions;

public class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(string id)
        : base("Book.NotFound", $"The book with id {id} was not found.")
    { }
}
