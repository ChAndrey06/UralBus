using DAL.Entities.PersonIdentity;

namespace DAL.Entities.KontragentIdentity;

public class CarrierIdentity : KontragentIdentity
{
    public ICollection<DriverPersonIdentity> Drivers { get; set; }

    public ICollection<Transport.Transport> Transports { get; set; }
    
    public ICollection<Trip.Trip> Trips { get; set; }
}