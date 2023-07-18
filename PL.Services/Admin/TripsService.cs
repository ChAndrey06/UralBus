using BLL.Interfaces;
using Common.Models;
using PL.Services.Admin.Models;
using PLTrip = PL.Entities.Trip.Trip;
using BLLTrip = BLL.Entities.Trip.Trip;
using PL.Services.Client.Models;

namespace PL.Services.Admin;

public class TripsService : BasePLAdminService<BLLTrip, PLTrip, FilterModel, Guid>
{
	public TripsService(IRepository<BLLTrip, Guid> repository) : base(repository)
	{
	}

	public override async Task<GetWrapper<IEnumerable<PLTrip>>> GetAsync(FilterModel filter)
	{
		var bllTripGetWrapper = await _repository.GetAsync(
			filter:r=>(filter.ExistKeys==null||filter.ExistKeys.Count==0||filter.ExistKeys.Contains(r.Id)),
			includeProperties: new [] { 
				"Transport",
				"Driver",
				"Driver.User",
				"Carrier",
				"TripRoute", 
				"TripRoute.TripRoutePoints", 
				"TripRoute.TripRoutePoints.RoutePoint" 
			}
		);

		var plTripGetWrapper = new GetWrapper<IEnumerable<PLTrip>>(
			_mapper.Map<IEnumerable<PLTrip>>(bllTripGetWrapper.Items),
			bllTripGetWrapper.TotalCount
		);

		return plTripGetWrapper;
	}
	
	
	public async Task<GetWrapper<IEnumerable<PLTrip>>> GetWithTripFilterAsync(TripFilterModel filter)
    {
        var bllTripGetWrapper = await _repository.GetAsync(
            includeProperties: new []
            {
	            "Transport",
	            "TripRoute",
	            "TripRoute.TripRoutePoints",
	            "TripRoute.TripRoutePoints.RoutePoint",
	            "Driver",
	            "Driver.User",
	            "Carrier"
            },
            filter: r =>
	            (filter.StartDate == null || r.DepartureTime.Date.Date == filter.StartDate.Value.Date)
	            && (filter.TripRouteId == null || r.TripRouteId == filter.TripRouteId)
        );

        var plTripGetWrapper = new GetWrapper<IEnumerable<PLTrip>>(
            _mapper.Map<IEnumerable<PLTrip>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;
    }

    public async Task<GetWrapper<IEnumerable<PLTrip>>> GetTripsSchedule(TripsFilterModel filter)
    {
        var bllTripGetWrapper = await _repository.GetAsync(
            includeProperties: new[] {
                "Transport",
                "Driver",
                "Driver.User",
                "Carrier",
                "TripRoute",
                "TripRoute.TripRoutePoints",
                "TripRoute.TripRoutePoints.RoutePoint"
            },
			filter: r => DateTime.Compare(r.DepartureTime, filter.EndTime) < 0
				&& DateTime.Compare(r.DepartureTime, filter.DepartureTime) > 0
				&& filter.TripRouteId == Guid.Empty || r.TripRouteId == filter.TripRouteId

        );

        var plTripGetWrapper = new GetWrapper<IEnumerable<PLTrip>>(
            _mapper.Map<IEnumerable<PLTrip>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;
    }

    public override async Task<PLTrip?> GetByIdAsync(Guid id)
	{
		var bllUser = await _repository.GetByIdAsync(
			id,
			includeProperties: new string[]
			{
				"Transport",
				"TripRoute",
				"TripRoute.TripRoutePoints",
				"TripRoute.TripRoutePoints.RoutePoint",
				"Driver",
				"Driver.User",
				"Carrier"
			}
		);

		return _mapper.Map<PLTrip>(bllUser);
	}
}
