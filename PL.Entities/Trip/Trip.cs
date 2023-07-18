using PL.Entities.KontragentIdentity;
using PL.Entities.PersonIdentity;
using PL.Entities.TripRoute;
using Common.Enums;

namespace PL.Entities.Trip;

public class Trip : BaseEntity
{
    public DateTime DepartureTime { get; set; }
    public decimal Price { get; set; }
    public Guid? CarrierId { get; set; }
    public Guid? TransportId { get; set; }
    public Guid? DriverId { get; set; }
	public Guid TripRouteId { get; set; }

    public int SeatsCount { get; set; }

	public TripRoute.TripRoute TripRoute { get; set; }
    public CarrierIdentity? Carrier { get; set; }
    public Transport.Transport? Transport { get; set; }
    public DriverPersonIdentity? Driver { get; set; }
    public ICollection<Order.Order> Orders { get; set; }

    public ICollection<TripRoutePoint>? TripRoutePoints => TripRoute?.TripRoutePoints;
    public RoutePoint.RoutePoint? ArrivalRoutePoint => TripRoutePoints?.FirstOrDefault(i => i.Type == RoutePointType.Arrival)?.RoutePoint;
    public RoutePoint.RoutePoint? DepartureRoutePoint => TripRoutePoints?.FirstOrDefault(i => i.Type == RoutePointType.Departure)?.RoutePoint;
    public DateTime? ArrivalTime 
    { 
        get 
        {
            if (TripRoutePoints == null) return null;

            var totalTravelTime = TimeSpan.FromMinutes(TripRoutePoints.Sum(r => r.MinutesFromStart));
            var arivalTime = DepartureTime + totalTravelTime;

            return arivalTime;
        } 
    }
    public ulong? TravelTime => (ulong?)TripRoutePoints?.Sum(r => r.MinutesFromStart);
    public int? TravelHours => TravelTime.HasValue ? TimeSpan.FromSeconds(TravelTime.GetValueOrDefault()).Hours : null;
    public int? TravelMinutes => TravelTime.HasValue ? TimeSpan.FromSeconds(TravelTime.GetValueOrDefault()).Minutes : null;
    public int? FreeSeats => Transport is null ? null : Transport.Seats - Orders.Count(r => new[] { OrderStatus.Active, OrderStatus.Booked }.Contains(r.Status));
    public TimeSpan? TravelTimeSpan => TravelTime.HasValue ? TimeSpan.FromSeconds(TravelTime.Value) : null;
}