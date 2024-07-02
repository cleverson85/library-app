using Application.Core.Contract;
using Domain.Entities;

namespace Application.Books.Queries.Filter;

public sealed class BookResponseFilter : CoreOperationResponse
{
    public List<Book> Books { get; set; } = [];

    public static explicit operator BookResponseFilter(List<Book> books) => new()
    {
        Books = books,
    };
}

