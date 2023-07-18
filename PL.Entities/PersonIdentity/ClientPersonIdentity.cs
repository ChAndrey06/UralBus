using Common.Enums;
using Common.Helpers;
using System.Text.Json.Serialization;

namespace PL.Entities.PersonIdentity;

public class ClientPersonIdentity : PersonIdentity
{

    public int? OrderCount => Orders?.Count;

    //public decimal? TotalSum => Orders?.Sum(o => (o.Price - o.Discount));

    public bool? ActiveBooking => Orders?.Any(o => o.Status == OrderStatus.Active);

    public string? Email => User?.Email;

    public string? PhoneNumber => User?.PhoneNumber;

    public int? ReturnCount => Orders?.Count(o => o?.Payment?.ReturnId != null);

    public bool Blacklisted { get; set; }

    public bool SendAdvertisements { get; set; } = false;

    public ICollection<Order.Order> Orders { get; set; }
}