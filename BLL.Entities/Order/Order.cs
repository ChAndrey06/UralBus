using BLL.Entities.PersonIdentity;
using BLL.Entities.TripRoute;
using Common.Attributes;
using Common.Enums;

namespace BLL.Entities.Order;

public class Order : BaseEntity
{
    [CanPatch]
    public int AdultsCount { get; set; }
    [CanPatch]
    public int ChildrenCount { get; set; }
    [CanPatch]
    public string Status { get; set; }
    [CanPatch]
    public string CreationType { get; set; }
    [CanPatch]
    public decimal Discount { get; set; }
    [CanPatch]
    public Guid TripId { get; set; }
    [CanPatch]
    public Guid StartTripRoutePointId { get; set; }
    [CanPatch]
    public Guid EndTripRoutePointId { get; set; }
    [CanPatch]
    public Guid ClientId { get; set; }
    [CanPatch]
    public Guid? PaymentId { get; set; }

    public Trip.Trip Trip { get; set; }
    public TripRoutePoint StartTripRoutePoint { get; set; }
    public TripRoutePoint EndTripRoutePoint { get; set; }
    public ClientPersonIdentity Client { get; set; }
    public Payment.Payment? Payment { get; set; }
    
    
    public string RefundType { get; set; }
    
    public string ReasonRefund { get; set; }
    
    [CanPatch]
    public string? ClientPhoneNumber { get; set; }
    [CanPatch]
    public string? ClientFirstName { get; set; }
    [CanPatch]
    public string? ClientLastName { get; set; }
    [CanPatch]
    public string? ClientPatronymic { get; set; }
    
}