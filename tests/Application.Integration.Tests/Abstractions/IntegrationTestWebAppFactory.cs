using Infrastructure.Contexts.MongoDb;
using Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using Testcontainers.Redis;

namespace Application.Integration.Tests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    MongoDbContainer _mongoContainer = new MongoDbBuilder()
          .WithUsername("root")
          .WithPassword("123456")
          .Build();

    RedisContainer _redisContainer = new RedisBuilder()
          .WithImage("redis:7.0")
          .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(MongoDbContext));
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(_mongoContainer.GetConnectionString());
            });

            services.RemoveAll(typeof(RedisOptions));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _redisContainer.GetConnectionString();
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _redisContainer.StartAsync();
        await _mongoContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _redisContainer.StopAsync();
        await _mongoContainer.StopAsync();
    }
}
