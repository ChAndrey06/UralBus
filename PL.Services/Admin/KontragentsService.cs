using PLKontragent = PL.Entities.KontragentIdentity.KontragentIdentity;
using BLLKontragent = BLL.Entities.KontragentIdentity.KontragentIdentity;
using PL.Services.Admin.Models;
using BLL.Interfaces;
using Common.Models;

namespace PL.Services.Admin;

public class KontragentsService : BasePLAdminService<BLLKontragent, PLKontragent, FilterModel, Guid>
{
    public KontragentsService(IRepository<BLLKontragent, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLKontragent>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";

        var bllKontragentGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: u => 
                u.Title.Contains(searchQuery) ||
                u.Discriminator.Contains(searchQuery)
        );

        var plKontragentGetWrapper = new GetWrapper<IEnumerable<PLKontragent>>(
            _mapper.Map<IEnumerable<PLKontragent>>(bllKontragentGetWrapper.Items),
            bllKontragentGetWrapper.TotalCount
        );

        return plKontragentGetWrapper;
    }

	public async Task<GetWrapper<IEnumerable<PLKontragent>>> GetAsync(string type)
	{
		var bllKontragentGetWrapper = await _repository.GetAsync(filter: u => u.Discriminator == type);

		var plKontragentGetWrapper = new GetWrapper<IEnumerable<PLKontragent>>(
			_mapper.Map<IEnumerable<PLKontragent>>(bllKontragentGetWrapper.Items),
			bllKontragentGetWrapper.TotalCount
		);
        
		return plKontragentGetWrapper;
	}

	public override async Task<PLKontragent?> GetByIdAsync(Guid id)
    {
        var bllKontragent = await _repository.GetByIdAsync(id);

        return _mapper.Map<PLKontragent>(bllKontragent);
    }
}
