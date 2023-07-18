using Common.Attributes;
using PL.Entities.KontragentIdentity;
using PL.Entities.PersonIdentity;
using DayOfWeek = Common.Enums.DayOfWeek;

namespace PL.Entities.TripDraft;

public class TripDraft:BaseEntity
{
	[CanPatch]
	public Guid TripRouteId { get; set; }

	[CanPatch]
	public TripRoute.TripRoute TripRoute { get; set; }

	[CanPatch]
	public Guid? DriverId { get; set; }

	[CanPatch]
	public DriverPersonIdentity? Driver { get; set; }

	[CanPatch]
	public Guid? CarrierId { get; set; }

	[CanPatch]
	public CarrierIdentity? Carrier { get; set; }

	[CanPatch]
	public Guid? TransportId { get; set; }

	[CanPatch]
	public Transport.Transport? Transport { get; set; }

    [CanPatch]
    public DayOfWeek StartDayOfWeek { get; set; }

	[CanPatch]
	public DayOfWeek? EndDayOfWeek { get; set; }

	[CanPatch]
	public string? StartTimeOfDay { get; set; }

	[CanPatch]
	public string? EndTimeOfDay { get; set; }

	[CanPatch]
    public decimal Price { get; set; }
}


