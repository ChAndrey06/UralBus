using DAL.Entities.KontragentIdentity;
using DAL.Entities.PersonIdentity;

namespace DAL.Entities.Transport;

public class Transport : BaseEntity
{
    public string Title { get; set; }

    public string Number { get; set; }
    public int Seats { get; set; }

    public Guid CarrierId { get; set; }
    public CarrierIdentity Carrier { get; set; }

    public Guid? DriverId { get; set; }
    public DriverPersonIdentity? Driver { get; set; }
}