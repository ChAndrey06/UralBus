using BLL.Interfaces;
using Common.Models;
using PL.Services.Client.Models;
using Common.Enums;
using PL.Entities.TripRoute;
using PL.Services.Admin.Models;
using PL.Services.Admin;
using PLTrip = PL.Entities.Trip.Trip;
using BLLTrip = BLL.Entities.Trip.Trip;
using DAL.Entities.Trip;

namespace PL.Services.Client;

public class TripsService : BasePLAdminService<BLLTrip, PLTrip, TripsFilterModel, Guid>
{
    public TripsService(IRepository<BLLTrip, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLTrip>>> GetAsync(TripsFilterModel filter)
    {
        var bllTripGetWrapper = await _repository.GetAsync(
            includeProperties: new []
            {
                "TripRoute", 
                "TripRoute.TripRoutePoints", 
                "TripRoute.TripRoutePoints.RoutePoint",
                "Transport"
            },
            filter: r =>
                r.DepartureTime.Date == filter.DepartureTime.Date &&
                r.TripRoute.TripRoutePoints.Any(p =>
                    p.RoutePointId == filter.FromRoutePointId &&
                    p.Type == RoutePointType.Departure.ToString()
                ) &&
                r.TripRoute.TripRoutePoints.Any(p =>
                    p.RoutePointId == filter.ToRoutePointId &&
                    p.Type == RoutePointType.Arrival.ToString()
                )// && r.DepartureTime > DateTime.UtcNow.AddHours(3)
        );

        var plTripGetWrapper = new GetWrapper<IEnumerable<PLTrip>>(
            _mapper.Map<IEnumerable<PLTrip>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        if (filter.OrderByModel != null)
        {
            switch (filter.OrderByModel.PropertyName)
            {
                case nameof(PL.Entities.Trip.Trip.DepartureTime):
                    plTripGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                        ? plTripGetWrapper.Items.OrderBy(r => r.DepartureTime)
                        : plTripGetWrapper.Items.OrderByDescending(r => r.DepartureTime);
                    break;
                case nameof(PL.Entities.Trip.Trip.ArrivalTime):
                    plTripGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                        ? plTripGetWrapper.Items.OrderBy(r => r.ArrivalTime)
                        : plTripGetWrapper.Items.OrderByDescending(r => r.ArrivalTime);
                    break;
                case nameof(PL.Entities.Trip.Trip.Price):
                    plTripGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                        ? plTripGetWrapper.Items.OrderBy(r => r.Price)
                        : plTripGetWrapper.Items.OrderByDescending(r => r.Price);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return plTripGetWrapper;
    }

    public override async Task<PLTrip?> GetByIdAsync(Guid id)
    {
        var bllUser = await _repository.GetByIdAsync(
            id, 
            includeProperties: new [] 
            { 
                "TripRoute", 
                "TripRoute.TripRoutePoints", 
                "TripRoute.TripRoutePoints.RoutePoint",
                "Carrier"
            }
        );

        return _mapper.Map<PLTrip>(bllUser);
    }

    public async Task<GetWrapper<IEnumerable<PLTrip>>> GetTripsCalendar(TripsFilterModel filter)
    {
        var bllTripGetWrapper = await _repository.GetAsync(
            includeProperties: new[]
            {
                "TripRoute",
                "TripRoute.TripRoutePoints",
                "TripRoute.TripRoutePoints.RoutePoint",
                "Transport"
            },
            filter: r =>               
                DateTime.Compare(r.DepartureTime, filter.DepartureTime.AddDays(7)) < 0
                && DateTime.Compare(r.DepartureTime, filter.DepartureTime.AddDays(-7)) > 0
                && filter.TripRouteId == Guid.Empty ? true : r.TripRouteId == filter.TripRouteId


        );

        var plTripGetWrapper = new GetWrapper<IEnumerable<PLTrip>>(
            _mapper.Map<IEnumerable<PLTrip>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;

    }

    public async Task<IEnumerable<TripRoutePointDataDto>> GetPointsByTripIdAsync(Guid id,bool detailed=false)
    {
        var trip = await _repository.GetByIdAsync(
            id,
            includeProperties: new []
            {
                "TripRoute.TripRoutePoints.RoutePoint"
            }
        );

        List<TripRoutePointDataDto> points = trip.TripRoute.TripRoutePoints
            .OrderBy(r => r.MinutesFromStart)
            .Select(r => detailed?new DetaliedTripRoutePointDataDto()
            {
                TripRoutePoint = _mapper.Map<TripRoutePoint>(r),
                Latitude = r.RoutePoint.Latitude, 
                Longitude = r.RoutePoint.Longitude,
                TripRoutePointId = r.Id,
                TripRoutePointTitle = r.RoutePoint.Title
            }: new TripRoutePointDataDto { 
                Latitude = r.RoutePoint.Latitude, 
                Longitude = r.RoutePoint.Longitude
            })
            .ToList();

        return points;
    }
}