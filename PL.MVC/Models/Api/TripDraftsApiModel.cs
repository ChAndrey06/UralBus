using PL.Entities.TripDraft;

namespace PL.MVC.Models.Api;

public class TripDraftApiModel
{
    public TripTimeApiModel Start { get; set; }
    public TripTimeApiModel? End { get; set; }
    public Guid? Id { get; set; }
    public Guid TripRouteId { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? CarrierId { get; set; }
    public Guid? TransportId { get; set; }
    
    public decimal Price { get; set; }


    public TripDraft ToPlModel()
    {
        return new TripDraft
        {
            Id = this.Id.GetValueOrDefault(),
            CreatedAt = default,
            IsDeleted = false,
            DeletedAt = null,
            TripRouteId = this.TripRouteId,
            DriverId = this.DriverId,
            CarrierId = this.CarrierId,
            TransportId = this.TransportId,
            StartDayOfWeek = this.Start.DayOfTheWeek,
            EndDayOfWeek = this.End?.DayOfTheWeek,
            StartTimeOfDay = this.Start.TimeOfDay,
            EndTimeOfDay = this.End?.TimeOfDay
            
        };
    }
}

public class TripTimeApiModel
{
    public Common.Enums.DayOfWeek DayOfTheWeek { get; set; }
    public string TimeOfDay { get; set; }
}