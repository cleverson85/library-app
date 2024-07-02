using Application.Books.Command.Create;
using Application.Books.Command.Delete;
using Application.Books.Command.Update;
using Application.Books.Queries.GetAll;
using Application.Books.Queries.GetById;
using Application.Books.Query.GetAll;
using Application.Books.Query.GetById;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.V1;

[Authorize]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/book")]
public class BookController : ApiController<BookController>
{
    [MapToApiVersion("1")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBookResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ICreateBookOperation operation, [FromBody] CreateBookRequest request, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(request, cancellationToken);
        return CustomResponse(result);
    }

    [MapToApiVersion("1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookResponseList>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(IGetAllBooksOperation operation, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(new BookRequestList(), cancellationToken);
        return CustomResponse(result);
    }

    [MapToApiVersion("1")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetId(IGetBookByIdOperation operation, string id, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(new BookRequest(id), cancellationToken);
        return CustomResponse(result);
    }

    [MapToApiVersion("1")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateBookResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(IUpdateBookOperation operation, UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(request, cancellationToken);
        return CustomResponse(result);
    }

    [MapToApiVersion("1")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteBookResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(IDeleteBookOperation operation, string id, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(new DeleteBookRequest(id), cancellationToken);
        return CustomResponse(result);
    }
}
