using Application.Books.Queries.GetAll;
using Application.Books.Query.GetAll;
using MinimalEndpoints.Abstractions;
using WebApp.Abstractions;

namespace WebApp.Endpoints;

public class Book : BaseEndpoint<Book>, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("book", async (IGetAllBooksOperation operation, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(new BookRequestList(), cancellationToken);
            return CustomResponse(result);
        })
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);
    }
}