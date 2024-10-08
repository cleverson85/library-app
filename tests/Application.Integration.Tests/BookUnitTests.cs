using Application.IntegrationTests.Abstractions;
using Domain.Entities;
using Application.Books.Queries.GetById;
using Application.Books.Command.Create;
using Microsoft.Extensions.DependencyInjection;
using Application.Books.Query.GetById;
using Application.Books.Command.Delete;
using Domain.Abstraction.Repositories;
using Application.Integration.Tests.Abstractions;
using Application.Books.Command.Update;

namespace Application.Integration.Tests;

public class BookUnitTests : BaseIntegrationTest
{
    public BookUnitTests(IntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Add_ValidBook_ShouldAdd()
    {
        //Arrange
        var request = new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "1234");

        //Act
        var response = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync((Book)request);
        var book = await UnitOfWork.GetRepository<IBookRepository>().GetByIdAsync(response.Id);

        //Assert
        Assert.NotNull(book);
        Assert.Equal(request.Author, book.Author);
        Assert.Equal(request.Title, book.Title);
    }

    [Fact]
    public async Task Add_InvalidBook_ShouldReturnInvalidResult()
    {
        //Arrange
        var request = new CreateBookRequest(string.Empty, Faker.Name.LastName(), "1234");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateBookOperation>();
        var response = await operation.ProcessAsync(request);

        //Assert
        Assert.True(response.Errors.Count > 0);
    }

    [Fact]
    public async Task Add_InvalidBookRequest_ShouldReturnInvalidResult_BookAlreadyExixts()
    {
        //Arrange
        var request1 = new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "88888");
        var request2 = new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "88888");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<ICreateBookOperation>();
        var response1 = await operation.ProcessAsync(request1);
        var response2 = await operation.ProcessAsync(request2);

        //Assert
        Assert.False(response2.IsValid);
        Assert.True(response2.Errors.Exists(c => c.PropertyName == "Book.AlreadyExists"));
    }

    [Fact]
    public async Task Get_BookNotExists_ShouldReturnBookNotFoundResult()
    {
        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<IGetBookByIdOperation>();
        var response = await operation.ProcessAsync(new BookRequest("295441EB0CAE29D61C007B2F"));

        //Assert
        Assert.Null(response.Book);
    }

    [Fact]
    public async Task Update_BookExists_ShouldUpdate()
    {
        //Arrange
        var request = new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "CLERRR");

        //Act
        var response = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync((Book)request);

        response.Author = "Author Updated";
        response.Title = "Title Updated";
        response.RegisterNumber = "AAACC65";

        await UnitOfWork.GetRepository<IBookRepository>().UpdateAsync(response);
        var result = await UnitOfWork.GetRepository<IBookRepository>().GetBookByRegisterNumber(response.RegisterNumber);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Author Updated", result.Author);
        Assert.Equal("Title Updated", result.Title);
        Assert.Equal("AAACC65", result.RegisterNumber);
    }

    [Fact]
    public async Task Update_BookNotExists_ShouldReturnIdEqualNull()
    {
        //Arrange
        var request = new UpdateBookRequest("6683427f7f74fad7d777179b", Faker.Name.FullName(), Faker.Name.LastName(), "CLEYYY");

        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<IUpdateBookOperation>();
        var result = await operation.ProcessAsync(request);

        //Assert
        Assert.True(result.Errors.Exists(c => c.PropertyName == "Book.NotFound"));
    }

    [Fact]
    public async Task Delete_BookExists_ShouldDelete()
    {
        //Arrange
        var request = new Book(Faker.Name.FullName(), Faker.Name.LastName(), "CLEZZZ");

        //Act
        var result = await UnitOfWork.GetRepository<IBookRepository>().SaveAsync(request);
        await UnitOfWork.GetRepository<IBookRepository>().DeleteAsync(result.Id);
        var book = await UnitOfWork.GetRepository<IBookRepository>().GetByIdAsync(result.Id);

        //Assert
        Assert.Null(book);
    }

    [Fact]
    public async Task Delete_BookNotExists_ShouldReturBookNotFound()
    {
        //Act
        var operation = _scope.ServiceProvider.GetRequiredService<IDeleteBookOperation>();
        var book = await operation.ProcessAsync(new DeleteBookRequest("668341e5f418271c6897ed90"));

        //Assert
        Assert.False(book.IsValid);
        Assert.True(book.Errors.Exists(c => c.PropertyName == "Book.NotFound"));
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllBooks()
    {
        //Arrange
        List<CreateBookRequest> createBooks = new() {
            new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "777aaa"),
            new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "666bbb"),
            new CreateBookRequest(Faker.Name.FullName(), Faker.Name.LastName(), "555vvv")
        };

        //Act
        createBooks.ForEach(async c => await UnitOfWork.GetRepository<IBookRepository>().SaveAsync((Book)c));
        var result = await UnitOfWork.GetRepository<IBookRepository>().GetAllAsync();

        //Assert
        Assert.Contains(result, c => c.RegisterNumber == "777aaa");
        Assert.Contains(result, c => c.RegisterNumber == "666bbb");
        Assert.Contains(result, c => c.RegisterNumber == "555vvv");
    }
}