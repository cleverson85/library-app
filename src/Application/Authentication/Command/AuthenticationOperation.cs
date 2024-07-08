using Application.Authentication.Exceptions;
using Domain.Core.Contract;
using Domain.Core.Operation;
using Application.Extensions;
using Application.Options;
using Application.Users.Exceptions;
using Domain.Abstraction;
using Domain.Abstraction.Repositories;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication.Command;

public sealed class AuthenticationOperation(IUnitOfWork unitOfWork, ILogger<CoreOperationAsync<UserRequest, UserResponse>> logger, 
    IOptions<JwtOptions> jwtOptions, IUserRepository userRepository) : CoreOperationAsync<UserRequest, UserResponse>(unitOfWork, logger), IAuthenticationOperation
{
    private readonly IOptions<JwtOptions> _jwtOptions = jwtOptions;
    private readonly IUserRepository _userRepository = userRepository;
    private User _user;

    private string GenerateToken(User request)
    {
        var jwt = _jwtOptions.Value;

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Secret)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            jwt.Issuer,
            jwt.Audience,
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, request.Id)
            },
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwt.ExpiryMinutes)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    protected override async Task<UserResponse> ProcessOperationAsync(UserRequest request, CancellationToken cancellationToken)
    {
        var result = await Task.Run(() =>
        {
            var token = GenerateToken(_user);
            return new UserResponse()
            {
                IsAuthenticaded = true,
                Token = token,
            };
        });

        return result;
    }

    protected override async Task<ValidationResult> ValidateAsync(UserRequest request, CancellationToken cancellationToken)
    {
        _user = await _unitOfWork.GetRepository<IUserRepository>().GetByUserName(request.UserName, cancellationToken);

        if (_user is null)
        {
            return new UserNotFoundException(request.UserName);
        }

        if (!request.Password.PassWordCheck(_user.Password))
        {
            return new UserInvalidLoginException(request.UserName);
        }

        return new CoreOperationResponse();
    }
}
