using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using MongoDB.Driver;

namespace Domain.Abstraction;

public interface IUnitOfWork
{
    IMongoDbContext Context { get; }
    IClientSessionHandle Session { get; }    
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
    T GetRepository<T>() where T : IRepository;
}
