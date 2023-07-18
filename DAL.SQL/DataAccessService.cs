using Common.Delta;
using Common.Interfaces;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

public class DataAccessService<TEntity,TKey> : IDataAccessService<TEntity,TKey>
    where TEntity : class, IEntity<TKey>
	where TKey : struct
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _context;

    public DataAccessService(IDatabaseConnection connection)
    {
        _context = connection.CreateDbContext();
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> Get()
    {
        return _dbSet.Where(r => !r.IsDeleted).AsQueryable().AsNoTracking();
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        var entity = await FindNotDeletedAsync(id);
		_context.Entry(entity).State = EntityState.Detached;

		return entity;
	}

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> items)
    {
        await _dbSet.BulkMergeAsync(items);
    }

    //public async Task<TEntity> UpdateAsync(TEntity entity)
    //{
    //    var entry = _context.Entry(entity);
    //    entry.State = EntityState.Modified;
         
    //    _dbSet.Update(entity);
    //    await _context.SaveChangesAsync();

    //    return entity;
    //}

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var existingEntity = await FindNotDeletedAsync(entity.Id);
        if (existingEntity != null)
        {
            DetachEntityGraph(existingEntity);
           // _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await FindNotDeletedAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<TEntity?> FindNotDeletedAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity is null || entity.IsDeleted ? null : entity;
    }
    
    private void DetachEntityGraph(object entity)
    {
        _context.Entry(entity).State = EntityState.Detached;

        var navigationEntries = _context.Entry(entity).Navigations;
        foreach (var navigationEntry in navigationEntries)
            {
                if (navigationEntry.CurrentValue is IEnumerable<object> navigationCollection)
                {
                    foreach (var navigationEntity in navigationCollection)
                    {
                        DetachEntityGraph(navigationEntity);
                    }
                }
                else if(navigationEntry.CurrentValue!=null)
                {
                    DetachEntityGraph(navigationEntry.CurrentValue);
                }
            }
    }
}