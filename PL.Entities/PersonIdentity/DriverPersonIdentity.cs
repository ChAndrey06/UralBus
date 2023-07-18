using PL.Entities.KontragentIdentity;

namespace PL.Entities.PersonIdentity;

public class DriverPersonIdentity : PersonIdentity
{
    public Guid CarrierId { get; set; }
	public CarrierIdentity? Carrier { get; set; }
}