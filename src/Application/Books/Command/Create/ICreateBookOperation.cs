using Application.Core.Abstraction;

namespace Application.Books.Command.Create;

public interface ICreateBookOperation : ICoreOperationAsync<CreateBookRequest, CreateBookResponse>
{ }
