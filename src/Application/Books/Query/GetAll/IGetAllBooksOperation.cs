using Application.Books.Queries.GetAll;
using Domain.Core.Abstraction;

namespace Application.Books.Query.GetAll;

public interface IGetAllBooksOperation : ICoreOperationAsync<BookRequestList, BookResponseList>
{ }
