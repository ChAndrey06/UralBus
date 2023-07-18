using Common.Enums;
using Common.Helpers;
using Newtonsoft.Json;

namespace PL.Entities.TripRoute;

public class TripRoutePoint : BaseEntity
{
    public Guid RoutePointId { get; set; }

    public Guid TripRouteId { get; set; }

    public RoutePointType Type { 
        get
        {
            return TypeString.ParseEnum<RoutePointType>();
        }
        set
        {
            TypeString = value.ToString();
        }
    }

    [JsonIgnore]
    public string TypeString { get; set; }

    public RoutePoint.RoutePoint RoutePoint { get; set; }

    public TripRoute TripRoute { get; set; }
    
    public int MinutesFromStart { get; set; }
    
    public decimal PriceForPoint { get; set; }
}
