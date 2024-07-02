using Domain.Abstraction.Repositories;
using Infrastructure.Repositories;

namespace WebApp;

public static class Repository
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
