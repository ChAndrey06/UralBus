namespace PL.MVC.Models;

public class CreateOrderViewModel
{
    public Guid TripId { get; set; }
    public Guid StartPointId { get; set; }
    public Guid EndPointId { get; set; }
    public int AdultsCount { get; set; }
    public int ChildrenCount { get; set; }
}
