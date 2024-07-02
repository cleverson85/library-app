using Application.Core.Contract;
using Domain.Entities;

namespace Application.Books.Command.Create;

public sealed class CreateBookRequest : CoreOperationRequest
{
    public CreateBookRequest(string author, string title, string registerNumber)
    {
        Author = author;
        Title = title;
        RegisterNumber = registerNumber;
    }

    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RegisterNumber { get; set; } = string.Empty;

    public static explicit operator Book(CreateBookRequest bookRequest) => new Book(bookRequest.Author, bookRequest.Title, bookRequest.RegisterNumber);
}
