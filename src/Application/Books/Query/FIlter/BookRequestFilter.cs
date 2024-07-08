using Application.Books.Query.FIlter;
using Domain.Core.Contract;

namespace Application.Books.Queries.Filter;

public sealed class BookRequestFilter : CoreOperationRequest
{
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string RegisterNumber { get; set; } = string.Empty;

    public static explicit operator BookRequestFilter(BookFilter filter) => new()
    {
        Author = filter.author,
        Title = filter.title,
        RegisterNumber = filter.registerNumber
    };
}