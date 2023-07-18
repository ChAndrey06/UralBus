using PLAuthCode = PL.Entities.AuthCode.AuthCode;
using BLLAuthCode = BLL.Entities.AuthCode.AuthCode;
using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;

namespace PL.Services.Admin;

public class AuthCodesService : BasePLAdminService<BLLAuthCode, PLAuthCode, FilterModel, Guid>
{
    public AuthCodesService(IRepository<BLLAuthCode, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLAuthCode>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";
        
        var bllAuthCodeGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: u => 
                u.User.Email.Contains(searchQuery) ||
                u.User.PhoneNumber.Contains(searchQuery),
			includeProperties: new[] { "User" }
		);

        var plAuthCodeGetWrapper = new GetWrapper<IEnumerable<PLAuthCode>>(
            _mapper.Map<IEnumerable<PLAuthCode>>(bllAuthCodeGetWrapper.Items),
            bllAuthCodeGetWrapper.TotalCount
        );

        return plAuthCodeGetWrapper;
    }

    public override async Task<PLAuthCode?> GetByIdAsync(Guid id)
    {
        var bllUser = await _repository.GetByIdAsync(id);

        return _mapper.Map<PLAuthCode>(bllUser);
    }
}
