using PLRoutePoint = PL.Entities.RoutePoint.RoutePoint;
using BLLRoutePoint = BLL.Entities.RoutePoint.RoutePoint;
using Common.Models;
using BLL.Interfaces;
using PL.Services.Admin.Models;

namespace PL.Services.Admin;

public class RoutePointsService : BasePLAdminService<BLLRoutePoint, PLRoutePoint, FilterModel, Guid>
{
    public RoutePointsService(IRepository<BLLRoutePoint, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLRoutePoint>>> GetAsync(FilterModel filter)
    {
        var bllRoutePointGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: u => u.Title.Contains(filter.SearchQuery ?? "")
        );

        var plRoutePointGetWrapper = new GetWrapper<IEnumerable<PLRoutePoint>>(
            _mapper.Map<IEnumerable<PLRoutePoint>>(bllRoutePointGetWrapper.Items),
            bllRoutePointGetWrapper.TotalCount
        );

        return plRoutePointGetWrapper;
    }

	public async Task<GetWrapper<IEnumerable<PLRoutePoint>>> GetAsync()
	{
		var bllRoutePointGetWrapper = await _repository.GetAsync();

		var plRoutePointGetWrapper = new GetWrapper<IEnumerable<PLRoutePoint>>(
			_mapper.Map<IEnumerable<PLRoutePoint>>(bllRoutePointGetWrapper.Items),
			bllRoutePointGetWrapper.TotalCount
		);

		return plRoutePointGetWrapper;
	}

	public override async Task<PLRoutePoint?> GetByIdAsync(Guid id)
    {
        var bllRoutePoint = await _repository.GetByIdAsync(id);

        return _mapper.Map<PLRoutePoint>(bllRoutePoint);
    }

	public async Task<PLRoutePoint?> GetByTitleAsync(string title)
	{
		var bllRoutePoint = (await _repository.GetAsync(filter: r => r.Title == title)).Items.FirstOrDefault();

		return _mapper.Map<PLRoutePoint>(bllRoutePoint);
	}
}
