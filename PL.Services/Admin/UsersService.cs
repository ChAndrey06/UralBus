using PLUser = PL.Entities.User.User;
using BLLUser = BLL.Entities.User.User;
using Common.Models;
using BLL.Interfaces;
using PL.Services.Admin.Models;

namespace PL.Services.Admin;

public class UsersService : BasePLAdminService<BLLUser, PLUser, UsersFilterModel, Guid>
{
	public UsersService(IRepository<BLLUser, Guid> repository) : base(repository)
	{
	}

	public override async Task<PLUser?> GetByIdAsync(Guid id)
	{
		var bllUser = await _repository.GetByIdAsync(id);

		return _mapper.Map<PLUser>(bllUser);
	}

	public override async Task<GetWrapper<IEnumerable<PLUser>>> GetAsync(UsersFilterModel filter)
	{
		var searchQuery = filter.SearchQuery ?? "";
		
		var bllUserGetWrapper = await _repository.GetAsync(
			limit: filter.Limit,
			offset: filter.Offset,
			filter: u => 
				u.Email.Contains(searchQuery) ||
				u.PhoneNumber.Contains(searchQuery)
		);

		var plUserGetWrapper = new GetWrapper<IEnumerable<PLUser>>(
			_mapper.Map<IEnumerable<PLUser>>(bllUserGetWrapper.Items),
			bllUserGetWrapper.TotalCount
		);

		return plUserGetWrapper;
	}
	public async Task<GetWrapper<IEnumerable<PLUser>>> GetAsync()
	{
		var bllUserGetWrapper = await _repository.GetAsync();

		var plUserGetWrapper = new GetWrapper<IEnumerable<PLUser>>(
			_mapper.Map<IEnumerable<PLUser>>(bllUserGetWrapper.Items),
			bllUserGetWrapper.TotalCount
		);

		return plUserGetWrapper;
	}
}