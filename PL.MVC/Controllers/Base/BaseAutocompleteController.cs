using AutoMapper;
using BLL.Interfaces;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.Mappings.MapperFactory;
using PL.MVC.Models;
using System.Linq.Expressions;

namespace PL.MVC.Controllers.Base;

public abstract class BaseAutocompleteController<TBLLEntity, TPLEntity, TSearchModel> : ControllerBase
    where TBLLEntity : class, IEntity<Guid>
    where TSearchModel : BaseAutocompleteSearchModel
{
    protected readonly IMapper _mapper;
    protected readonly IRepository<TBLLEntity, Guid> _repository;
    protected string[] IncludeProperties { get; set; }

    public BaseAutocompleteController(IRepository<TBLLEntity, Guid> repository)
    {
        _repository = repository;
        _mapper = MapperFactory.GetMapper();
    }

    public virtual async Task<GetWrapper<IEnumerable<TPLEntity>>> GetAsync(TSearchModel model)
    {
        var bllGetWrapper = await _repository.GetAsync(
            filter: GetFilterExpression(model),
            includeProperties: IncludeProperties,
            limit: model.Limit
        );

        var plGetWrapper = new GetWrapper<IEnumerable<TPLEntity>>(
            _mapper.Map<IEnumerable<TPLEntity>>(bllGetWrapper.Items),
            bllGetWrapper.TotalCount
        );

        return plGetWrapper;
    }

    protected abstract Expression<Func<TBLLEntity, bool>> GetFilterExpression(TSearchModel model);
}
