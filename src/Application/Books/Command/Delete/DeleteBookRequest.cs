using Application.Core.Contract;

namespace Application.Books.Command.Delete;

public sealed class DeleteBookRequest(string id) : CoreOperationRequest
{
    public string Id { get; set; } = id;
}
