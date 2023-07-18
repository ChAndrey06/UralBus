namespace PL.Services.Admin.Models;
using DayOfWeek= Common.Enums.DayOfWeek;
public class TripDraftsFilterModel:FilterModel
{
    public DayOfWeek? StartDayOfWeek { get; set; }
    
    public DayOfWeek? EndDayOfWeek { get; set; }
    
    public Guid? TripRouteId { get; set; }
}