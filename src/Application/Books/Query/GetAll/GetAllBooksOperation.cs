using Application.Books.Query.GetAll;
using Domain.Abstraction;
using Domain.Abstraction.Repositories;
using Domain.Core.Operation;
using Microsoft.Extensions.Logging;

namespace Application.Books.Queries.GetAll;

public sealed class GetAllBooksOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequestList, BookResponseList>> logger) 
    : CoreOperationAsync<BookRequestList, BookResponseList>(unitOfWork, logger), IGetAllBooksOperation
{ 
    protected override async Task<BookResponseList> ProcessOperationAsync(BookRequestList request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.GetRepository<IBookRepository>().GetAllAsync();
        return (BookResponseList)books;
    }
}
