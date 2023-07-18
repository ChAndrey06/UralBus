using DAL.Entities.PersonIdentity;
using DAL.Entities.TripRoute;

namespace DAL.Entities.Order;

public class Order : BaseEntity
{
   public Guid TripId { get; set; }
   
   public Trip.Trip Trip { get; set; }
   
   public Guid StartTripRoutePointId { get; set; }
   
   public TripRoutePoint StartTripRoutePoint { get; set; }
   
   public Guid EndTripRoutePointId { get; set; }
   
   public TripRoutePoint EndTripRoutePoint { get; set; }
   
   public ClientPersonIdentity Client { get; set; }
      
   public Guid ClientId { get; set; }
   
   public int AdultsCount { get; set; }
   
   public int ChildrenCount { get; set; }
   
   public string Status { get; set; }
   
   public string CreationType { get; set; }
   
   public decimal Discount { get; set; }
   
   public Payment.Payment? Payment { get; set; }
   
   public Guid? PaymentId { get; set; }
   
   public string? ReasonRefund { get; set; }
   
   public string? ClientPhoneNumber { get; set; }
   public string? ClientFirstName { get; set; }
   public string? ClientLastName { get; set; }
   public string? ClientPatronymic { get; set; }
}