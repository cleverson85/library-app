using Application.Books.Queries.GetById;
using Domain.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;
using Domain.Abstraction.Repositories;

namespace Application.Books.Query.GetById;

public sealed class GetBookByIdOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequest, BookResponse>> logger) 
    : CoreOperationAsync<BookRequest, BookResponse>(unitOfWork, logger), IGetBookByIdOperation
{
    protected override async Task<BookResponse> ProcessOperationAsync(BookRequest request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(request.Id, cancellationToken);
        return (BookResponse)book;
    }
}
