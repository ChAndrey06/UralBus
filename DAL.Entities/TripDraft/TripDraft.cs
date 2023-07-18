using DAL.Entities.KontragentIdentity;
using DAL.Entities.PersonIdentity;
using DayOfWeek = Common.Enums.DayOfWeek;

namespace DAL.Entities.TripDraft;

public class TripDraft:BaseEntity
{
    public Guid TripRouteId { get; set; }
    
    public TripRoute.TripRoute TripRoute { get; set; }
    
    public Guid? DriverId { get; set; }
    
    public DriverPersonIdentity? Driver { get; set; }
    
    public Guid? CarrierId { get; set; }
    
    public CarrierIdentity? Carrier { get; set; }
    
    public Guid? TransportId { get; set; }
    
    public Transport.Transport? Transport { get; set; }
    
    public DayOfWeek StartDayOfWeek { get; set; }
    
    public DayOfWeek? EndDayOfWeek { get; set; }
    
    public string? StartTimeOfDay { get; set; }
    
    public string? EndTimeOfDay { get; set; }
    
    public decimal Price { get; set; }
}


