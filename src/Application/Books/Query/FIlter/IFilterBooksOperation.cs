using Application.Books.Queries.Filter;
using Application.Core.Abstraction;

namespace Application.Books.Query.Filter;

public interface IFilterBooksOperation : ICoreOperationAsync<BookRequestFilter, BookResponseFilter>
{ }
