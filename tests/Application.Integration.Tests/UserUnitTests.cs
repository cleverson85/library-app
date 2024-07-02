using Application.IntegrationTests.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Application.Users.Command.Create;
using Application.Users.Command.Update;

namespace Application.Integration.Tests;

public class UserUnitTests : BaseIntegrationTest
{
    public UserUnitTests(WebApplicationFactory<Program> factory) : base(factory)
    { }

    [Fact]
    public async Task Add_ValidUser_ShouldAdd()
    {
        //Arrange
        var request = new CreateUserRequest(Faker.Internet.UserName(), "123456");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        await operation.ProcessAsync(request);

        var response = await UnitOfWork.UserRepository.GetByUserName(request.UserName);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(request.UserName, response.UserName);
    }

    [Fact]
    public async Task Add_InvalidUser_ShouldReturnInvalidResult()
    {
        //Arrange
        var request = new CreateUserRequest(string.Empty, "123456");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        var response = await operation.ProcessAsync(request);

        //Assert
        Assert.True(response.Errors.Count > 0);
    }

    [Fact]
    public async Task Add_InvalidUserRequest_InvalidResult()
    {
        //Arrange
        var request = new CreateUserRequest(string.Empty, string.Empty);

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        var response = await operation.ProcessAsync(request);

        //Assert
        Assert.True(response.Errors.Count > 0);
    }

    [Fact]
    public async Task Add_InvalidUserRequest_ShouldReturnInvalidResult_UserlreadyExixts()
    {
        //Arrange
        string userName = Faker.Internet.UserName();
        var request1 = new CreateUserRequest(userName, "123456");
        var request2 = new CreateUserRequest(userName, "123456");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateUserOperation>();
        var response1 = await operation.ProcessAsync(request1);
        var response2 = await operation.ProcessAsync(request2);

        //Assert
        Assert.False(response2.IsValid);
        Assert.True(response2.Errors.Exists(c => c.PropertyName == "User.AlreadyExists"));
    }

    [Fact]
    public async Task Delete_UserExists_ShouldDelete()
    {
        //Arrange
        var request = new CreateUserRequest(Faker.Internet.UserName(), "123456");

        //Act
        var result = await UnitOfWork.UserRepository.SaveAsync((User)request);
        await UnitOfWork.UserRepository.DeleteAsync(result.Id);
        var user = await UnitOfWork.UserRepository.GetByIdAsync(result.Id);

        //Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task Delete_UserNotExists_ShouldReturUserNotFound()
    {
        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<IDeleteUserOperation>();
        var user = await operation.ProcessAsync(new DeleteUserRequest("6683427f7f74fad7d777179b"));

        //Assert
        Assert.False(user.IsValid);
        Assert.True(user.Errors.Exists(c => c.PropertyName == "User.NotFound"));
    }
}