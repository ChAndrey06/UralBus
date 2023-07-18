namespace DAL.Entities.PersonIdentity;

public class ClientPersonIdentity : PersonIdentity
{
    public bool Blacklisted { get; set; } = false;
    
    public ICollection<Order.Order> Orders { get; set; }

    public bool SendAdvertisements { get; set; } = false;
}