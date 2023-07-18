using PL.Entities.PersonIdentity;

namespace PL.Entities.KontragentIdentity;

public class CarrierIdentity:KontragentIdentity
{
    public ICollection<DriverPersonIdentity> Drivers { get; set; }
    
    public ICollection<Transport.Transport> Transports { get; set; }
}