using Application.Books.Query.Filter;
using Domain.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;
using Domain.Abstraction.Repositories;

namespace Application.Books.Queries.Filter;

public sealed class FilterBooksOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequestFilter, BookResponseFilter>> logger) 
    : CoreOperationAsync<BookRequestFilter, BookResponseFilter>(unitOfWork, logger), IFilterBooksOperation
{ 
    protected override async Task<BookResponseFilter> ProcessOperationAsync(BookRequestFilter request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.GetRepository<IBookRepository>().GetBookByFilter(request);
        return (BookResponseFilter)books.ToList();
    }
}
