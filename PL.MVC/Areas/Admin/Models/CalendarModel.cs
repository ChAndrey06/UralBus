using Common.Models;
using PL.Entities.TripDraft;
using PL.Entities.TripRoute;
using PL.Services.Admin.Models;

namespace PL.MVC.Areas.Admin.Models
{
	public class CalendarModel
	{
		public GetWrapper<IEnumerable<TripRoute>> TripRoutes { get; set; }
		public GetWrapper<IEnumerable<TripDraft>> TripDrafts { get; set; }

		public TripDraftsFilterModel Filter { get; set; }
	}
}
