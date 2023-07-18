using BLL.Interfaces;
using Common.Models;
using PL.Services.Admin.Models;
using PLTripRoutePoint = PL.Entities.TripRoute.TripRoutePoint;
using BLLTripRoutePoint = BLL.Entities.TripRoute.TripRoutePoint;

namespace PL.Services.Admin;

public class TripRoutePointsService : BasePLAdminService<BLLTripRoutePoint, PLTripRoutePoint, FilterModel, Guid>
{
    public TripRoutePointsService(IRepository<BLLTripRoutePoint, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLTripRoutePoint>>> GetAsync(FilterModel filter)
    {
        var bllTripRoutePointGetWrapper = await _repository.GetAsync(
            includeProperties: new [] {
                "TripRoute",
                "RoutePoint"
            }
        );

        var plTripRoutePointGetWrapper = new GetWrapper<IEnumerable<PLTripRoutePoint>>(
            _mapper.Map<IEnumerable<PLTripRoutePoint>>(bllTripRoutePointGetWrapper.Items),
            bllTripRoutePointGetWrapper.TotalCount
        );

        return plTripRoutePointGetWrapper;
    }

    public override async Task<PLTripRoutePoint?> GetByIdAsync(Guid id)
    {
        var bllTripRoutePoint = await _repository.GetByIdAsync(
            id,
            includeProperties: new []
            {
                "TripRoute",
                "RoutePoint"
            }
        );

        return _mapper.Map<PLTripRoutePoint>(bllTripRoutePoint);
    }
}
