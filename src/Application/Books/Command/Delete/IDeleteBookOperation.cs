using Domain.Core.Abstraction;

namespace Application.Books.Command.Delete;

public interface IDeleteBookOperation : ICoreOperationAsync<DeleteBookRequest, DeleteBookResponse>
{ }
