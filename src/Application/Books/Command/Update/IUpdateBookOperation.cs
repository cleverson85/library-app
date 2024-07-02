using Application.Core.Abstraction;

namespace Application.Books.Command.Update;

public interface IUpdateBookOperation : ICoreOperationAsync<UpdateBookRequest, UpdateBookResponse>
{ }
