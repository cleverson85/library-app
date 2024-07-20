using Application.Users.Command.Create;
using Application.Users.Command.Delete;
using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints.Abstractions;
using WebApp.Abstractions;

namespace WebApp.Endpoints.V1;  

public class User : BaseEndpoint<User>, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user", async (ICreateUserOperation operation, [FromBody] CreateUserRequest request, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(request, cancellationToken);
            return CustomResponse(result);
        })
        .Accepts<CreateUserRequest>("application/json")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags(EndpointSchema.UserTag)
        .MapToApiVersion(1);

        app.MapDelete("user/{id}", async (IDeleteUserOperation operation, string id, CancellationToken cancellationToken) =>
        {
            var result = await operation.ProcessAsync(new DeleteUserRequest(id), cancellationToken);
            return CustomResponse(result);
        })
        .WithTags(EndpointSchema.UserTag)
        .MapToApiVersion(1);
    }
}
