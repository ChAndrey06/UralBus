using PLFAQ = PL.Entities.FAQ.FAQ;
using BLLFAQ = BLL.Entities.FAQ.FAQ;
using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;

namespace PL.Services.Admin;

public class FAQsService : BasePLAdminService<BLLFAQ, PLFAQ, FilterModel, Guid>
{
	public FAQsService(
        IRepository<BLLFAQ, Guid> repository
	) : base(repository)
    {
	}

    public override async Task<GetWrapper<IEnumerable<PLFAQ>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";

        var bllFAQGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: u => 
                u.Question.Contains(searchQuery) ||
                u.Category.Title.Contains(searchQuery),
            includeProperties: new[] { "Category" }
        );

        var plFAQGetWrapper = new GetWrapper<IEnumerable<PLFAQ>>(
            _mapper.Map<IEnumerable<PLFAQ>>(bllFAQGetWrapper.Items),
            bllFAQGetWrapper.TotalCount
        );

        return plFAQGetWrapper;
    }

    public override async Task<PLFAQ?> GetByIdAsync(Guid id)
    {
        var bllFAQ = await _repository.GetByIdAsync(id, includeProperties: new[] { "Category" });

        return _mapper.Map<PLFAQ>(bllFAQ);
    }
}
