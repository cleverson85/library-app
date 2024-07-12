using Application.Options;
using Infrastructure.Options;
using WebApp.OpenApi;

namespace WebApp.DependencyInjection;

public static class OptionsPattern
{
    public static void Register(IServiceCollection services)
    {
        services.ConfigureOptions<DbOptionsSetup>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<RedisOptionsSetup>();
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();
    }
}
