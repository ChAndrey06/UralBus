using PLFAQCategory = PL.Entities.FAQ.FAQCategory;
using BLLFAQCategory = BLL.Entities.FAQ.FAQCategory;
using PL.Services.Admin.Models;
using BLL.Interfaces;
using Common.Models;

namespace PL.Services.Admin;

public class FAQCategoriesService : BasePLAdminService<BLLFAQCategory, PLFAQCategory, FilterModel, Guid>
{
	public FAQCategoriesService(IRepository<BLLFAQCategory, Guid> repository) : base(repository)
	{
	}

	public override async Task<GetWrapper<IEnumerable<PLFAQCategory>>> GetAsync(FilterModel filter)
	{
		var bllFAQCategoryGetWrapper = await _repository.GetAsync(
			limit: filter.Limit,
			offset: filter.Offset,
			filter: u => u.Title.Contains(filter.SearchQuery ?? "")
		);

		var plFAQCategoryGetWrapper = new GetWrapper<IEnumerable<PLFAQCategory>>(
			_mapper.Map<IEnumerable<PLFAQCategory>>(bllFAQCategoryGetWrapper.Items),
			bllFAQCategoryGetWrapper.TotalCount
		);

		return plFAQCategoryGetWrapper;
	}

	public async Task<GetWrapper<IEnumerable<PLFAQCategory>>> GetAsync()
	{
		var bllFAQCategoryGetWrapper = await _repository.GetAsync();

		var plFAQCategoryGetWrapper = new GetWrapper<IEnumerable<PLFAQCategory>>(
			_mapper.Map<IEnumerable<PLFAQCategory>>(bllFAQCategoryGetWrapper.Items),
			bllFAQCategoryGetWrapper.TotalCount
		);

		return plFAQCategoryGetWrapper;
	}

	public override async Task<PLFAQCategory?> GetByIdAsync(Guid id)
	{
		var bllUser = await _repository.GetByIdAsync(id);

		return _mapper.Map<PLFAQCategory>(bllUser);
	}
}
