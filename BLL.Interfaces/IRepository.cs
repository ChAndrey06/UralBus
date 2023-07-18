using System.Linq.Expressions;
using Common.Delta;
using Common.Interfaces;
using Common.Models;

namespace BLL.Interfaces;

/// <summary>
/// Generic репозиторий для доступа к CRUD операциям.
/// </summary>
/// <typeparam name="TBllEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TBllEntity, in TKey> where TBllEntity : class, IEntity<TKey>
    where TKey : struct
{
    /// <summary>
    /// Получить сущности. Поддержка пагинации, фильтрации, сортировки и включения связанных сущностей (пока что временно нет поддержки подгрузки нескольких уровней вложенности).
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties">Желательно передавать названия через nameof()</param>
    /// <returns></returns>
    Task<GetWrapper<IEnumerable<TBllEntity>>> GetAsync(
        int? limit = null,
        int? offset = null,
        Expression<Func<TBllEntity, bool>>? filter = null,
       // Func<IQueryable<TBllEntity>, IOrderedQueryable<TBllEntity>>? orderBy = null,
        IEnumerable<string> includeProperties = null
    );

    /// <summary>
    /// Получить сущность по id. Поддержка включения связанных сущностей (пока что временно нет поддержки подгрузки нескольких уровней вложенности).
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<TBllEntity?> GetByIdAsync(
        TKey id,
        IEnumerable<string>? includeProperties = null);

    Task<TBllEntity> AddAsync(TBllEntity entity);

    Task<TBllEntity> UpdateAsync(TBllEntity entity);

    Task<TBllEntity> PatchAsync(TKey id, Delta<TBllEntity> delta);

    Task DeleteAsync(TKey id, bool soft = false);

    public Task UpdateRangeAsync(IEnumerable<TBllEntity> items);
}