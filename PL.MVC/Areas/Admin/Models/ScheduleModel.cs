using PL.Entities.Trip;
using PL.Entities.TripRoute;

namespace PL.MVC.Areas.Admin.Models
{
	public class ScheduleModel
	{
		public List<TripRoute> TripRoutes { get; set; }
		public List<Trip> Trips { get; set; }
		public IndexInfoDay[] datesBetween { get; set; }
		public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public Guid ChooseTripRouteId { get; set; }
	}
}
