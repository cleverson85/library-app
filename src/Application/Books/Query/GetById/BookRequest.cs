using Domain.Core.Contract;

namespace Application.Books.Queries.GetById;

public sealed class BookRequest(string id) : CoreOperationRequest
{
    public string Id { get; set; } = id;
}
