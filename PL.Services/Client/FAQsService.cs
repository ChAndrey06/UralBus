using AutoMapper;
using BLL.Interfaces;
using Common.Models;
using PL.Mappings.MapperFactory;
using PL.Services.Client.Models;
using BLLFAQ = BLL.Entities.FAQ.FAQ;
using PLFAQ = PL.Entities.FAQ.FAQ;
using BLLFAQCategory = BLL.Entities.FAQ.FAQCategory;
using PLFAQCategory = PL.Entities.FAQ.FAQCategory;

namespace PL.Services.Client;

public class FAQsService
{
	protected readonly IMapper _mapper;
	protected readonly IRepository<BLLFAQ, Guid> _repository;
	protected readonly IRepository<BLLFAQCategory, Guid> _categoriesRepository;

	public FAQsService(
		IRepository<BLLFAQ, Guid> repository,
		IRepository<BLLFAQCategory, Guid> categoriesRepository
	)
	{
		_repository = repository;
		_categoriesRepository = categoriesRepository;
		_mapper = MapperFactory.GetMapper();
	}

	public async Task<GetWrapper<IEnumerable<PLFAQ>>> GetAsync(FAQsFilterModel filter)
	{
		var bllFAQGetWrapper = await _repository.GetAsync(
			filter: r => 
				r.Question.Contains(filter.SearchQuery ?? "") &&
				r.CategoryId == filter.CategoryId,
			limit: filter.Limit,
			offset: filter.Offset
		);

		var plFAQGetWrapper = new GetWrapper<IEnumerable<PLFAQ>>(
			_mapper.Map<IEnumerable<PLFAQ>>(bllFAQGetWrapper.Items),
			bllFAQGetWrapper.TotalCount
		);

		return plFAQGetWrapper;
	}

	public async Task<GetWrapper<IEnumerable<PLFAQCategory>>> GetCategoriesAsync()
	{
		var bllFAQCategoryGetWrapper = await _categoriesRepository.GetAsync();

		var plFAQCategoryGetWrapper = new GetWrapper<IEnumerable<PLFAQCategory>>(
			_mapper.Map<IEnumerable<PLFAQCategory>>(bllFAQCategoryGetWrapper.Items),
			bllFAQCategoryGetWrapper.TotalCount
		);

		return plFAQCategoryGetWrapper;
	}
}
