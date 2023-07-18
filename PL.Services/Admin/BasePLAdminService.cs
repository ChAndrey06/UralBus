using AutoMapper;
using BLL.Interfaces;
using Common.Delta;
using Common.Interfaces;
using Common.Models;
using PL.Mappings.MapperFactory;
using PL.Services.Admin.Models;

namespace PL.Services.Admin;

public abstract class BasePLAdminService<TBllEntity, TPLEntity, TFilterModel, TKey>
    where TKey : struct
    where TPLEntity : class, IEntity<TKey>
    where TBllEntity : class, IEntity<TKey>
    where TFilterModel : class, IFilterModel
{
    protected readonly IMapper _mapper;
    protected readonly IRepository<TBllEntity, TKey> _repository;

    protected BasePLAdminService(IRepository<TBllEntity, TKey> repository)
    {
        _repository = repository;
        _mapper = MapperFactory.GetMapper();
    }

    public abstract Task<GetWrapper<IEnumerable<TPLEntity>>> GetAsync(TFilterModel filter);

    public abstract Task<TPLEntity?> GetByIdAsync(TKey id);

    public virtual async Task<TPLEntity> AddAsync(TPLEntity entity)
    {
        var bllEntity = _mapper.Map<TBllEntity>(entity);
        var result = await _repository.AddAsync(bllEntity);

        return _mapper.Map<TPLEntity>(result);
    }

    public virtual async Task<TPLEntity> UpdateAsync(TPLEntity entity)
    {
        var bllEntity = _mapper.Map<TBllEntity>(entity);
        var result = await _repository.UpdateAsync(bllEntity);

        return _mapper.Map<TPLEntity>(result);
    }

    public async Task UpdateRangeAsync(IEnumerable<TPLEntity> items)
    {
        var bls = _mapper.Map<IEnumerable<TBllEntity>>(items);
        await _repository.UpdateRangeAsync(bls);
    }

    public virtual async Task<TPLEntity> PatchAsync(TKey id, Delta<TPLEntity> delta)
    {
        var bllEntity = await _repository.GetByIdAsync(id);
        if (bllEntity == null)
        {
            throw new KeyNotFoundException();
        }
        var patchedEntity = _mapper.Map<TPLEntity>(bllEntity);

        delta.Patch(patchedEntity);

		bllEntity = _mapper.Map<TBllEntity>(patchedEntity);

		await _repository.UpdateAsync(bllEntity);

        return patchedEntity;
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        await _repository.DeleteAsync(id);
    }
}