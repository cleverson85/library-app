using Application.Books.Queries.GetById;
using Domain.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;
using Domain.Abstraction.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Application.Extensions;

namespace Application.Books.Query.GetById;

public sealed class GetBookByIdOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<BookRequest, BookResponse>> logger, IDistributedCache cache) 
    : CoreOperationAsync<BookRequest, BookResponse>(unitOfWork, logger), IGetBookByIdOperation
{
    private readonly IDistributedCache _cache = cache;

    protected override async Task<BookResponse> ProcessOperationAsync(BookRequest request, CancellationToken cancellationToken)
    {
        var result = await _cache.GetOrCreateAsync($"book-{request.Id}", async () =>
        {
            var book = (BookResponse)await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(request.Id, cancellationToken);
            return book;
        });

        return result;
    }
}
