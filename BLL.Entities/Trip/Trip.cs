using BLL.Entities.KontragentIdentity;
using BLL.Entities.PersonIdentity;

namespace BLL.Entities.Trip;

public class Trip : BaseEntity
{
    public decimal Price { get; set; }
    public DateTime DepartureTime { get; set; }
    public Guid TripRouteId { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? CarrierId { get; set; }
    public Guid? TransportId { get; set; }

    public TripRoute.TripRoute TripRoute { get; set; }
    public CarrierIdentity? Carrier { get; set; }
    public Transport.Transport? Transport { get; set; }
    public DriverPersonIdentity? Driver { get; set; }
    public ICollection<Order.Order> Orders { get; set; }
}