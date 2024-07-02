using Application.Authentication.Command;
using Application.Books.Command.Create;
using Application.Books.Command.Delete;
using Application.Books.Command.Update;
using Application.Books.Queries.Filter;
using Application.Books.Queries.GetAll;
using Application.Books.Query.Filter;
using Application.Books.Query.GetAll;
using Application.Books.Query.GetById;
using Application.Users.Command.Create;
using Application.Users.Command.Update;

namespace WebApp.DependencyInjection;

public static class Operaions
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<ICreateBookOperation, CreateBookOperation>();
        services.AddScoped<IUpdateBookOperation, UpdateBookOperation>();
        services.AddScoped<IDeleteBookOperation, DeleteBookOperation>();
        services.AddScoped<IGetAllBooksOperation, GetAllBooksOperation>();
        services.AddScoped<IGetBookByIdOperation, GetBookByIdOperation>();
        services.AddScoped<ICreateUserOperation, CreateUserOperation>();
        services.AddScoped<IDeleteUserOperation, DeleteUserOperation>();
        services.AddScoped<IFilterBooksOperation, FilterBooksOperation>();
        services.AddScoped<IAuthenticationOperation, AuthenticationOperation>();
    }
}
