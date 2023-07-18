using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.TripRoute;

public class TripRoutePoint : BaseEntity
{
    public Guid RoutePointId { get; set; }
    
    public Guid TripRouteId { get; set; }

    [Column("RoutePointType")]
    public string Type { get; set; }

    public RoutePoint.RoutePoint RoutePoint { get; set; }
    
    public Entities.TripRoute.TripRoute TripRoute { get; set; }
    
    public int MinutesFromStart { get; set; }

    public decimal PriceForPoint { get; set; } = 0;
}