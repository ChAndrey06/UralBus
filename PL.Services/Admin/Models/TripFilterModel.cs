namespace PL.Services.Admin.Models;

public class TripFilterModel : FilterModel
{
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public Guid? TripRouteId { get; set; }
}