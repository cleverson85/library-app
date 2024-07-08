namespace Domain.Abstraction.Repositories;

public interface IBaseRepository<TEntity> : IRepository where TEntity : BaseEntity, IAggregationRoot
{
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<long> DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
