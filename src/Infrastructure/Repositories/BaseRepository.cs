using Domain;
using Domain.Abstraction;
using Domain.Abstraction.MongoDbContext;
using Domain.Abstraction.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, IAggregationRoot
{
    protected readonly IMongoCollection<TEntity> _collection;

    public BaseRepository(IMongoDbContext context)
    {
        _collection = context.GetCollection<TEntity>();
    }

    public async Task<long> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(c => c.Id == id, cancellationToken);
        return result.DeletedCount;
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
    {
        return await _collection.AsQueryable()
                                .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _collection.FindSync(c => c.Id == id)
                                .FirstOrDefaultAsync();
    }

    public async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, null, cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
        return entity;
    }

}
