using Domain.Abstraction;
using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using MongoDB.Driver;

namespace Infrastructure;

public sealed class UnitOfWork(IMongoDbContext context, IBookRepository bookRepository, IUserRepository userRepository) : IUnitOfWork
{
    public IMongoDbContext Context { get; private set; } = context;
    public IClientSessionHandle Session { get; private set; }
    public IBookRepository BookRepository { get; private set; } = bookRepository;
    public IUserRepository UserRepository { get; private set; } = userRepository;

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
}
