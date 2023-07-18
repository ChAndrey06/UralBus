namespace PL.MVC.Models.Profile;

public class ClientViewModel:ProfileViewModel
{
    public int OrdersCount { get; set; }
    
    public int ReturnsCount { get; set; }
    
    public decimal TotalSum { get; set; }
    
    public bool ActiveBooking { get; set; }
    
    public bool Blacklisted { get; set; }
}