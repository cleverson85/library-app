using Application.Authentication.Command;
using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints.Abstractions;
using WebApp.Abstractions;

namespace WebApp.Endpoints.V1
{
    public class Authentication : BaseEndpoint<User>, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth", async (IAuthenticationOperation operation, [FromBody] UserRequest request, CancellationToken cancellationToken) =>
            {
                var result = await operation.ProcessAsync(request, cancellationToken);
                return CustomResponse(result);
            })
            .WithTags(EndpointSchema.AuthTag)
            .MapToApiVersion(1);
        }
    }
}
