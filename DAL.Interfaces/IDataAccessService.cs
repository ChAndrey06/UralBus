using Common.Interfaces;

namespace DAL.Interfaces;

public interface IDataAccessService<TEntity, TKey> where TKey : struct where TEntity : class, IEntity<TKey>
{
    IQueryable<TEntity> Get();
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);

    public Task UpdateRangeAsync(IEnumerable<TEntity> items);
}
