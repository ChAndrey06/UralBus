using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using PL.Entities.File;

namespace PL.Entities.TripRoute;

public class TripRoute : BaseEntity
{
    public string Title { get; set; }
    public RoutePoint.RoutePoint? ArrivalRoutePoint => TripRoutePoints?.FirstOrDefault(i => i.Type == Common.Enums.RoutePointType.Arrival)?.RoutePoint;
        
    public RoutePoint.RoutePoint? DepartureRoutePoint => TripRoutePoints?.FirstOrDefault(i => i.Type == Common.Enums.RoutePointType.Departure)?.RoutePoint;
    
    public bool PopularDestination { get; set; } = false;
    
    public S3File? PopularDestinationPicture { get; set; }
    public string? PopularDestinationPicturePath { get; set; }
    
    public Guid? PopularDestinationPictureId { get; set; }
    
    public string PopularDestinatonDescription { get; set; }

    public ICollection<TripRoutePoint> TripRoutePoints { get; set; }
    
    public string PopularDestinationHref => PopularDestination&&ArrivalRoutePoint!=null&&DepartureRoutePoint!=null?
        $"/Trips?FromRoutePointId={DepartureRoutePoint.Id}&ToRoutePointId={ArrivalRoutePoint.Id}&DepartureTime={DateTime.Now.AddDays(1).Date.ToString("yyyy-M-dd")}":"#";
}