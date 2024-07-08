using Infrastructure.Contexts.MongoDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Application.Integration.Tests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    MongoDbContainer _mongoContainer = new MongoDbBuilder()
          .WithUsername("root")
          .WithPassword("123456")
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
        });
    }

    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mongoContainer.StopAsync();
    }
}
