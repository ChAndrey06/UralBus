using BLL.Entities.PersonIdentity;

namespace BLL.Entities.KontragentIdentity;

public class CarrierIdentity:KontragentIdentity
{
    public ICollection<DriverPersonIdentity> Drivers { get; set; }
    
    public ICollection<Transport.Transport> Transports { get; set; }
    
    public ICollection<Trip.Trip> Trips { get; set; }
}