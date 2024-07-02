using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using MongoDB.Driver;

namespace Domain.Abstraction;

public interface IUnitOfWork
{
    IMongoDbContext Context { get; }
    IClientSessionHandle Session { get; }
    IBookRepository BookRepository { get; }
    IUserRepository UserRepository { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
}
