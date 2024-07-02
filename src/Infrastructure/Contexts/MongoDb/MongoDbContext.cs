using Domain;
using Domain.Abstraction;
using Domain.Abstraction.MongoDbContext;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Contexts.MongoDb;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<DbOptions> options, IMongoClient mongoClient)
    {
        var databaseOptions = options.Value;
        _database = mongoClient.GetDatabase(databaseOptions.DatabaseName);
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity, IAggregationRoot
    {
        return _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public IMongoClient GetClient()
    {
        return _database.Client;
    }
}
