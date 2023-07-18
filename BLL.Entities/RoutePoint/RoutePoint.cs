using Common.Attributes;

namespace BLL.Entities.RoutePoint;

public class RoutePoint : BaseEntity
{
    [CanPatch]
    public string Title { get; set; }
    
    [CanPatch]
    public string Address { get; set; }
    
    [CanPatch]
    public string Latitude { get; set; }
    
    [CanPatch]
    public string Longitude { get; set; }
}