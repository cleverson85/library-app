using MongoDB.Driver;

namespace Domain.Abstraction.MongoDbContext;

public interface IMongoDbContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity, IAggregationRoot;
    IMongoClient GetClient();
}
