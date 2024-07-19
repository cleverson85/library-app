//using Application.Authentication.Command;
//using Asp.Versioning;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApp.Controllers;

//[ApiVersion("1")]
//[Route("api/v{version:apiVersion}/auth")]
//public class AuthController : BaseEndpoint<AuthController>
//{
//    [MapToApiVersion("1")]
//    [HttpPost]
//    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> Login(IAuthenticationOperation operation, [FromBody] UserRequest request, CancellationToken cancellationToken)
//    {
//        var result = await operation.ProcessAsync(request, cancellationToken);
//        return CustomResponse(result);
//    }
//}
