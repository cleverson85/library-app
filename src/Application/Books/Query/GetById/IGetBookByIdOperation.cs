using Application.Books.Queries.GetById;
using Application.Core.Abstraction;

namespace Application.Books.Query.GetById;

public interface IGetBookByIdOperation : ICoreOperationAsync<BookRequest, BookResponse>
{ }
