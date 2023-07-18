using PL.Entities.TripRoute;

namespace PL.Services.Client.Models;

public class TripRoutePointDataDto
{
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}

public class DetaliedTripRoutePointDataDto : TripRoutePointDataDto
{
    public TripRoutePoint TripRoutePoint { get; set; }
    public Guid TripRoutePointId { get; set; }
    public string TripRoutePointTitle { get; set; }
}
