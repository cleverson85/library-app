using Domain.Core.Operation;
using Domain.Abstraction;
using Microsoft.Extensions.Logging;
using Domain.Abstraction.Repositories;

namespace Application.Books.Command.Delete;

public sealed class DeleteBookOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<DeleteBookRequest, DeleteBookResponse>> logger)
    : CoreOperationAsync<DeleteBookRequest, DeleteBookResponse>(unitOfWork, logger), IDeleteBookOperation
{
    protected override async Task<DeleteBookResponse> ProcessOperationAsync(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GetRepository<IBookRepository>().DeleteAsync(request.Id);
        if (result <= 0)
        {
            var response = new DeleteBookResponse();
            response.AddError("Book.NotFound", $"The book with id {request.Id} was not found.");
            return response;
        }

        return new DeleteBookResponse();
    }
}
