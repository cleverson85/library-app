using Application.Books.Query.Filter;
using Application.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;

namespace Application.Books.Queries.Filter;

public sealed class FilterBooksOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequestFilter, BookResponseFilter>> logger) 
    : CoreOperationAsync<BookRequestFilter, BookResponseFilter>(unitOfWork, logger), IFilterBooksOperation
{ 
    protected override async Task<BookResponseFilter> ProcessOperationAsync(BookRequestFilter request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.BookRepository.GetBookByFilter(request.Author, request.Title, request.RegisterNumber);
        return (BookResponseFilter)books;
    }
}
