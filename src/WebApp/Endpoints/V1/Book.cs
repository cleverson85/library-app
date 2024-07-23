using Application.Books.Command.Create;
using Application.Books.Command.Delete;
using Application.Books.Command.Update;
using Application.Books.Queries.GetAll;
using Application.Books.Queries.GetById;
using Application.Books.Query.GetAll;
using Application.Books.Query.GetById;
using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints.Abstractions;
using WebApp.Abstractions;

namespace WebApp.Endpoints.V1;

public class Book : BaseEndpoint<Book>, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("book", async (ICreateBookOperation operation, [FromBody] CreateBookRequest request, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(request, cancellationToken);
            return CustomResponse(result);
        })
        .RequireAuthorization()
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);

        app.MapPut("book", async (IUpdateBookOperation operation, [FromBody] UpdateBookRequest request, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(request, cancellationToken);
            return CustomResponse(result);
        })
        .RequireAuthorization()
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);

        app.MapGet("book", async (IGetAllBooksOperation operation, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(new BookRequestList(), cancellationToken);
            return CustomResponse(result);
        })
        .RequireAuthorization()
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);

        app.MapGet("book/{id}", async (IGetBookByIdOperation operation, string id, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(new BookRequest(id), cancellationToken);
            return CustomResponse(result);
        })
        .RequireAuthorization()
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);

        app.MapDelete("book/{id}", async (IDeleteBookOperation operation, string id, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(new DeleteBookRequest(id), cancellationToken);
            return CustomResponse(result);
        })
        .RequireAuthorization()
        .WithTags(EndpointSchema.BookTag)
        .MapToApiVersion(1);
    }
}