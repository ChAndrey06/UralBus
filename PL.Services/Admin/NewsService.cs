using PLNews = PL.Entities.News.News;
using BLLNews = BLL.Entities.News.News;
using Common.Models;
using BLL.Interfaces;
using PL.Services.Admin.Models;

namespace PL.Services.Admin;

public class NewsService : BasePLAdminService<BLLNews, PLNews, FilterModel, Guid>
{
	public NewsService(IRepository<BLLNews, Guid> repository) : base(repository)
	{
	}

	public override async Task<GetWrapper<IEnumerable<PLNews>>> GetAsync(FilterModel filter)
	{
		var bllNewsGetWrapper = await _repository.GetAsync(
			limit: filter.Limit,
			offset: filter.Offset,
			filter: u => u.Title.Contains(filter.SearchQuery ?? "")
		);

		var plNewsGetWrapper = new GetWrapper<IEnumerable<PLNews>>(
			_mapper.Map<IEnumerable<PLNews>>(bllNewsGetWrapper.Items),
			bllNewsGetWrapper.TotalCount
		);

		return plNewsGetWrapper;
	}

	public override async Task<PLNews?> GetByIdAsync(Guid id)
	{
		var bllNews = await _repository.GetByIdAsync(id, includeProperties: new [] { "File" });

		return _mapper.Map<PLNews>(bllNews);
	}
}
