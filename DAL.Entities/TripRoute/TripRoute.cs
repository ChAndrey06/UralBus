using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Files;

namespace DAL.Entities.TripRoute;

[Table(nameof(TripRoute))]
public class TripRoute : BaseEntity
{
    public string Title { get; set; }

    public bool PopularDestination { get; set; } = false;
    
    public S3File? PopularDestinationPicture { get; set; }
    
    public Guid? PopularDestinationPictureId { get; set; }
    
    public string PopularDestinatonDescription { get; set; }

    public ICollection<TripRoutePoint> TripRoutePoints { get; set; }
}