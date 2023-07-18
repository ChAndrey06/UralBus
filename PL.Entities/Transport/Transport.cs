using PL.Entities.KontragentIdentity;
using PL.Entities.PersonIdentity;

namespace PL.Entities.Transport;

public class Transport : BaseEntity
{
    public string Title { get; set; }
    public string Number { get; set; }
    public int Seats { get; set; }
    public Guid CarrierId { get; set; }
    public Guid? DriverId { get; set; }

    public CarrierIdentity Carrier { get; set; }
    public DriverPersonIdentity? Driver { get; set; }
}