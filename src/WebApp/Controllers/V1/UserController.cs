using Application.Users.Command.Create;
using Application.Users.Command.Update;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.V1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/user")]
public class UserController : BaseEndpoint<UserController>
{
    [HttpPost]
    [MapToApiVersion("1")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ICreateUserOperation operation, [FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(request, cancellationToken);
        return CustomResponse(result?.User?.Id);
    }

    [Authorize]
    [HttpDelete]
    [MapToApiVersion("1")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(IDeleteUserOperation operation, DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var result = await operation.ProcessAsync(request, cancellationToken);
        return CustomResponse(result);
    }
}
