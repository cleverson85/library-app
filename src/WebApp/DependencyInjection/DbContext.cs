using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApp.DependencyInjection;

public static class DbContext
{
    public static void Register(IServiceCollection services)
    {
        var databaseOptions = services.BuildServiceProvider()
                                      .GetService<IOptions<DbOptions>>()!.Value;

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(databaseOptions.ConnectionString);
        });
    }
}
