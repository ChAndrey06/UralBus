using AutoMapper;
using BLL.Interfaces;
using Common.Models;
using PL.Mappings.MapperFactory;
using PL.Services.Admin.Models;
using BLLNews = BLL.Entities.News.News;
using PLNews = PL.Entities.News.News;

namespace PL.Services.Client;

public class NewsService
{
    protected readonly IMapper _mapper;
    protected readonly IRepository<BLLNews, Guid> _repository;

    public NewsService(
        IRepository<BLLNews, Guid> repository
    )
    {
        _repository = repository;
        _mapper = MapperFactory.GetMapper();
    }

    public async Task<GetWrapper<IEnumerable<PLNews>>> GetAsync(FilterModel filter)
    {
        var bllNewsGetWrapper = await _repository.GetAsync(            
            limit: filter.Limit,
            offset: filter.Offset,
            filter: r => true,
            includeProperties: new[] { "File" } 
        );

        var plNewsGetWrapper = new GetWrapper<IEnumerable<PLNews>>(
            _mapper.Map<IEnumerable<PLNews>>(bllNewsGetWrapper.Items),
            bllNewsGetWrapper.TotalCount
        );

        return plNewsGetWrapper;
    }
}
