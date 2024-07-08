using Application.Authentication.Command;
using Application.Integration.Tests.Abstractions;
using Application.IntegrationTests.Abstractions;
using Application.Users.Command.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Integration.Tests;

public class AuthUnitTests : BaseIntegrationTest
{
    public AuthUnitTests(IntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Auth_ValidUser_Authenticated_ReturnToken()
    {
        //Arrange
        var request = new CreateUserRequest(Faker.Internet.UserName(), "123456");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        var result = await operation.ProcessAsync(request);

        var auth = _scope.ServiceProvider.GetRequiredService<IAuthenticationOperation>();
        var user = new UserRequest(result.User.UserName, "123456");
        var authReponse = await auth.ProcessAsync(user);

        //Assert
        Assert.NotNull(authReponse);
        Assert.True(authReponse.IsValid);
        Assert.True(authReponse.IsAuthenticaded);
    }

    [Fact]
    public async Task Auth_ValidUser_InvalidPassword()
    {
        //Arrange
        var user = new CreateUserRequest("Michael_Jackson", "123456");
        var auth = new UserRequest("Michael_Jackson", "CCCCC");

        //Act
        var operationUser = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        await operationUser.ProcessAsync(user);

        var operationAuth = _scope.ServiceProvider.GetRequiredService<IAuthenticationOperation>();
        var result = await operationAuth.ProcessAsync(auth);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Exists(c => c.PropertyName == "User.InvalidLogin"));
    }
}
