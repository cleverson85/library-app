using Application.Core.Contract;

namespace Application.Books.Queries.Filter;

public sealed class BookRequestFilter(string author, string title, string registerNumber) : CoreOperationRequest
{
    public string Author { get; set; } = author;
    public string Title { get; set; } = title;
    public string RegisterNumber { get; set; } = registerNumber;
}