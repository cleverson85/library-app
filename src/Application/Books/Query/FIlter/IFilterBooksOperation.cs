using Application.Books.Queries.Filter;
using Domain.Core.Abstraction;

namespace Application.Books.Query.Filter;

public interface IFilterBooksOperation : ICoreOperationAsync<BookRequestFilter, BookResponseFilter>
{ }
