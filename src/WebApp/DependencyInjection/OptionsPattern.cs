using Application.Options;
using Infrastructure.Options;

namespace WebApp.DependencyInjection;

public static class OptionsPattern
{
    public static void Register(IServiceCollection services)
    {
        services.ConfigureOptions<DbOptionsSetup>();
        services.ConfigureOptions<JwtOptionsSetup>();
    }
}
