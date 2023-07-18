using PL.Entities.Trip;

namespace PL.MVC.Areas.Admin.Models
{
    public class IndexInfoDay
    {
        public DateTime Date { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
