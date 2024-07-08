using Domain.Core.Abstraction;

namespace Application.Books.Command.Update;

public interface IUpdateBookOperation : ICoreOperationAsync<UpdateBookRequest, UpdateBookResponse>
{ }
