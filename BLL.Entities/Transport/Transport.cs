using BLL.Entities.KontragentIdentity;
using BLL.Entities.PersonIdentity;
using Common.Attributes;

namespace BLL.Entities.Transport;

public class Transport : BaseEntity
{
    [CanPatch]
    public string Title { get; set; }

    [CanPatch]
    public string Number { get; set; }
    
    [CanPatch]
    public int Seats { get; set; }

    public Guid CarrierId { get; set; }
    public CarrierIdentity Carrier { get; set; }

    public Guid? DriverId { get; set; }
    public DriverPersonIdentity? Driver { get; set; }
}