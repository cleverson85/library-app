using Domain.Abstraction;
using Domain.Abstraction.MongoDbContext;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure;

public sealed class UnitOfWork(IMongoDbContext context, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public IMongoDbContext Context { get; private set; } = context;
    public IClientSessionHandle Session { get; private set; }

    public async Task BeginTransactionAsync()
    {
        Session = await Context.GetClient().StartSessionAsync();
        Session.StartTransaction();
    }

    public async Task RollBackTransactionAsync()
    {
        await Session.AbortTransactionAsync();
        Session.Dispose();
    }

    public async Task CommitTransactionAsync()
    {
        await Session.CommitTransactionAsync();
        Session.Dispose();
    }

    public T GetRepository<T>() where T : IRepository
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
