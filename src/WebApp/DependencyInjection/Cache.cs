using Infrastructure.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace WebApp.DependencyInjection;

public static class Cache
{
    public static void Register(IServiceCollection services)
    {
        var redisOptions = services.BuildServiceProvider().GetService<IOptions<RedisOptions>>()!.Value;
        services.AddSingleton<IConnectionMultiplexer>(options => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { redisOptions.ConnectionString },
            AbortOnConnectFail = false,
            Ssl = redisOptions.Ssl
        }));
    }
}
