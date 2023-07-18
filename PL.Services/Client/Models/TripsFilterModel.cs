using PL.Services.Admin.Models;

namespace PL.Services.Client.Models;

public class TripsFilterModel : FilterModel
{
    public Guid FromRoutePointId { get; set; }
    public Guid ToRoutePointId { get; set; }
    public Guid TripRouteId { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime EndTime { get; set; }
}
