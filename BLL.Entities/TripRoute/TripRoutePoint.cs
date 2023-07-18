using Common.Attributes;

namespace BLL.Entities.TripRoute;

public class TripRoutePoint : BaseEntity
{
    [CanPatch]
    public Guid RoutePointId { get; set; }

    [CanPatch]
    public Guid TripRouteId { get; set; }

    [CanPatch]
    public string Type { get; set; }

    [CanPatch]
    public RoutePoint.RoutePoint RoutePoint { get; set; }

    [CanPatch]
    public Entities.TripRoute.TripRoute TripRoute { get; set; }
    
    [CanPatch]
    public int MinutesFromStart { get; set; }
    
    [CanPatch]
    public decimal PriceForPoint { get; set; } = 0;
}
