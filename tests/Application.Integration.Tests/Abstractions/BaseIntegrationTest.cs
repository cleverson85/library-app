using Application.Integration.Tests.Abstractions;
using Bogus;
using Domain.Abstraction;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    protected readonly IServiceScope _scope;
    protected Faker Faker { get; }
    protected HttpClient HttpClient { get; }
    protected IUnitOfWork UnitOfWork { get; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Faker = new Faker();
        HttpClient = factory.CreateClient();
        UnitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
    }
}
