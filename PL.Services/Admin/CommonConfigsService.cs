using PLConfig = PL.Entities.CommonConfigs.CommonConfiguration;
using BLLConfig = BLL.Entities.CommonConfigs.CommonConfiguration;
using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;

namespace PL.Services.Admin;

public class CommonConfigsService : BasePLAdminService<BLLConfig, PLConfig, FilterModel, Guid>
{
    public CommonConfigsService(IRepository<BLLConfig, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLConfig>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";

        var bllConfigGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: u => u.Key.Contains(searchQuery)
        );

        var plConfigGetWrapper = new GetWrapper<IEnumerable<PLConfig>>(
            _mapper.Map<IEnumerable<PLConfig>>(bllConfigGetWrapper.Items),
            bllConfigGetWrapper.TotalCount
        );

        return plConfigGetWrapper;
    }

    public override async Task<PLConfig?> GetByIdAsync(Guid id)
    {
        var bllConfig = await _repository.GetByIdAsync(id);

        return _mapper.Map<PLConfig>(bllConfig);
    }

    public async Task<PLConfig?> GetByKeyAsync(string key)
    {
        var bllConfig = (
            await _repository.GetAsync(filter: r => r.Key.Equals(key))
        ).Items?.FirstOrDefault();

        return _mapper.Map<PLConfig>(bllConfig);
    }
}
