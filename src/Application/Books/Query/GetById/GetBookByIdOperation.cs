using Application.Books.Queries.GetById;
using Application.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;

namespace Application.Books.Query.GetById;

public sealed class GetBookByIdOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequest, BookResponse>> logger) 
    : CoreOperationAsync<BookRequest, BookResponse>(unitOfWork, logger), IGetBookByIdOperation
{
    protected override async Task<BookResponse> ProcessOperationAsync(BookRequest request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(request.Id, cancellationToken);
        return (BookResponse)book;
    }
}
