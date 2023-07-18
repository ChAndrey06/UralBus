using BLL.Entities.KontragentIdentity;
using BLL.Entities.PersonIdentity;
using Common.Attributes;
using DayOfWeek= Common.Enums.DayOfWeek;
namespace BLL.Entities.TripDraft;

public class TripDraft:BaseEntity
{
    [CanPatch]
    public Guid TripRouteId { get; set; }
    
    public TripRoute.TripRoute TripRoute { get; set; }
    
    [CanPatch]
    
    public Guid? DriverId { get; set; }
    
    public DriverPersonIdentity? Driver { get; set; }
    [CanPatch]
    public Guid? CarrierId { get; set; }
    
    public CarrierIdentity? Carrier { get; set; }
    [CanPatch]
    public Guid? TransportId { get; set; }
    
    public Transport.Transport? Transport { get; set; }
    [CanPatch]
    public DayOfWeek StartDayOfWeek { get; set; }
    [CanPatch]
    public DayOfWeek? EndDayOfWeek { get; set; }
    [CanPatch]
    public string? StartTimeOfDay { get; set; }
    [CanPatch]
    public string? EndTimeOfDay { get; set; }
    
    [CanPatch]
    public decimal Price { get; set; }
}


