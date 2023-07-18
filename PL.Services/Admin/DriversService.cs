using PLDriver = PL.Entities.PersonIdentity.DriverPersonIdentity;
using BLLDriver = BLL.Entities.PersonIdentity.DriverPersonIdentity;
using PL.Services.Admin.Models;
using BLL.Interfaces;
using Common.Models;

namespace PL.Services.Admin;

public class DriversService : BasePLAdminService<BLLDriver, PLDriver, FilterModel, Guid>
{
	public DriversService(IRepository<BLLDriver, Guid> repository) : base(repository)
	{
	}

	public override async Task<GetWrapper<IEnumerable<PLDriver>>> GetAsync(FilterModel filter)
	{
		var searchQuery = filter.SearchQuery ?? "";

		var bllDriverGetWrapper = await _repository.GetAsync(
			limit: filter.Limit,
			offset: filter.Offset,
			includeProperties: new[] { "Carrier", "User" }
		);

		var plDriverGetWrapper = new GetWrapper<IEnumerable<PLDriver>>(
			_mapper.Map<IEnumerable<PLDriver>>(bllDriverGetWrapper.Items),
			bllDriverGetWrapper.TotalCount
		);

		return plDriverGetWrapper;
	}

	public async Task<GetWrapper<IEnumerable<PLDriver>>> GetAsync()
	{

		var bllDriverGetWrapper = await _repository.GetAsync(includeProperties: new[] { "Carrier", "User" });

		var plDriverGetWrapper = new GetWrapper<IEnumerable<PLDriver>>(
			_mapper.Map<IEnumerable<PLDriver>>(bllDriverGetWrapper.Items),
			bllDriverGetWrapper.TotalCount
		);

		return plDriverGetWrapper;
	}

	public override async Task<PLDriver?> GetByIdAsync(Guid id)
	{
		var bllDriver = await _repository.GetByIdAsync(
			id,
			includeProperties: new[] { "Carrier", "User" }
		);

		return _mapper.Map<PLDriver>(bllDriver);
	}
}
