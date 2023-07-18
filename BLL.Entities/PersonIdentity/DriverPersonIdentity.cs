using BLL.Entities.KontragentIdentity;

namespace BLL.Entities.PersonIdentity;

public class DriverPersonIdentity : PersonIdentity
{
    public Guid CarrierId { get; set; }

    public CarrierIdentity? Carrier { get; set; }
    
    public ICollection<Trip.Trip> Trips { get; set; }
}