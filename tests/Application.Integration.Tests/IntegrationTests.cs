using Application.Authentication.Command;
using Application.Books.Command.Update;
using Application.Books.Queries.GetById;
using Application.Integration.Tests.Abstractions;
using Application.IntegrationTests.Abstractions;
using Application.Users.Command.Create;
using Domain.Abstraction.Repositories;
using Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Application.Integration.Tests;

public class IntegrationTests : BaseIntegrationTest
{
    JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    public IntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Add_AuthenticatedUser_AddBook_ReturnOk()
    {
        //Arrange
        var request = new CreateUserRequest("Michael_Jackson", "123456");
        var book = new Book(Faker.Internet.UserName(), Faker.Name.LastName(), Faker.Random.AlphaNumeric(8));

        //Act
        //Create User
        var userReponse = await HttpClient.PostAsJsonAsync($"/api/v1/user", request);

        //Authenticate User
        var httpResponse = await HttpClient.PostAsJsonAsync($"/api/v1/auth", new UserRequest(request.UserName, "123456"));

        var stringContent = await SetResponseContent(httpResponse, book);

        //Http Post
        var response = await HttpClient.PostAsync($"/api/v1/book", stringContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_AuthenticatedUser_GetBookById_ReturnOk()
    {
        //Arrange
        var request = new CreateUserRequest("Michael_Jackson", "123456");
        var book = new Book(Faker.Internet.UserName(), "Lord Of the Rings", Faker.Random.AlphaNumeric(8));

        //Act
        //Create User
        var userReponse = await HttpClient.PostAsJsonAsync($"/api/v1/user", request);

        //Create Book
        var result = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync(book);

        //Authenticate User
        var httpResponse = await HttpClient.PostAsJsonAsync($"/api/v1/auth", new UserRequest(request.UserName, "123456"));

        await SetResponseContent(httpResponse, book);

        //Http Get
        var response = await HttpClient.GetAsync($"/api/v1/book/{result.Id}");
        var bookResult = await JsonSerializer.DeserializeAsync<BookResponse>(response.Content.ReadAsStream(), jsonOptions);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.Equal("Lord Of the Rings", bookResult?.Book?.Title);
    }

    [Fact]
    public async Task Update_AuthenticatedUser_UpdateBook_ReturnOk()
    {
        //Arrange
        var request = new CreateUserRequest("Michael_Jackson", "123456");
        var regiterNUmber = Faker.Random.AlphaNumeric(8);
        var book = new Book(Faker.Internet.UserName(), "Lord Of the Rings", regiterNUmber);

        //Act
        //Create User
        var userReponse = await HttpClient.PostAsJsonAsync($"/api/v1/user", request);

        //Create Book
        var result = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync(book);

        //Update Book 
        result.Author = "Jordan Peterson";
        result.Title = "Seven Live Rules";

        //Authenticate User
        var httpResponse = await HttpClient.PostAsJsonAsync($"/api/v1/auth", new UserRequest(request.UserName, "123456"));

        var stringContent = await SetResponseContent(httpResponse, result);

        //Http Update
        var response = await HttpClient.PutAsync($"/api/v1/book", stringContent);
        var bookResult = await JsonSerializer.DeserializeAsync<UpdateBookResponse>(response.Content.ReadAsStream(), jsonOptions);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.Equal("Seven Live Rules", bookResult?.Book?.Title);
        Assert.Equal(regiterNUmber, bookResult?.Book?.RegisterNumber);
    }

    [Fact]
    public async Task Delete_AuthenticatedUser_DeleteBook_ReturnOk()
    {
        //Arrange
        var request = new CreateUserRequest("Michael_Jackson", "123456");
        var book = new Book(Faker.Internet.UserName(), "Lord Of the Rings", Faker.Random.AlphaNumeric(8));

        //Act
        //Create User
        var userReponse = await HttpClient.PostAsJsonAsync($"/api/v1/user", request);

        //Create Book
        var result = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync(book);

        //Authenticate User
        var httpResponse = await HttpClient.PostAsJsonAsync($"/api/v1/auth", new UserRequest(request.UserName, "123456"));

        await SetResponseContent(httpResponse, result);

        //Http Delete
        var response = await HttpClient.DeleteAsync($"/api/v1/book/{result.Id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.Null(await UnitOfWork.GetRepository<IBookRepository>().GetByIdAsync(result.Id));
    }

    [Fact]
    public async Task Get_NotAuthenticatedUser_GetBooks_ShoudReturnUnauthorized()
    {
        //Arrange
        var request = new CreateUserRequest("Michael_Jackson", "123456");

        //Act
        //Create User
        var userReponse = await HttpClient.PostAsJsonAsync($"/api/v1/user", request);

        //Http Delete
        var response = await HttpClient.GetAsync($"/api/v1/book");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    private async Task<StringContent> SetResponseContent(HttpResponseMessage httpResponse, Book book)
    {
        var content = await httpResponse.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserResponse>(content, jsonOptions);

        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token);
        var postData = JsonSerializer.Serialize(book);

        return new StringContent(postData, Encoding.UTF8, "application/json");
    }
}
