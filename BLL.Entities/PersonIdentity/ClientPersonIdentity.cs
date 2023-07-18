using Common.Attributes;
using Common.Enums;

namespace BLL.Entities.PersonIdentity;

public class ClientPersonIdentity : PersonIdentity
{
    
    //@TODO to PL entity
    // public int? OrderCount => Orders?.Count;
    //
    // public decimal? TotalSum => Orders?.Sum(o => (o.Price - o.Discount));
    //
    // public bool? ActiveBooking => Orders?.Any(o => o.Status == OrderStatus.Active);
    //
    // public string? Email => User?.Email;
    //
    // public string? PhoneNumber => User?.PhoneNumber;
    //
    // public int? ReturnCount => Orders?.Count(o => o?.Payment?.ReturnId != null);
    //
    [CanPatch]
    public bool Blacklisted { get; set; } = false;
    
    public ICollection<Order.Order> Orders { get; set; }

    [CanPatch]
    public bool SendAdvertisements { get; set; } = false;
}