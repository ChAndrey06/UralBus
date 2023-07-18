using DAL.Entities.KontragentIdentity;

namespace DAL.Entities.PersonIdentity;

public class DriverPersonIdentity : PersonIdentity
{
    public string Title { get; set; }
    public Guid? CarrierId { get; set; }
    public CarrierIdentity? Carrier { get; set; }
    
    public ICollection<Trip.Trip> Trips { get; set; }
}