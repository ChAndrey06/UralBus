using BLL.Entities.TripRoute;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class TripRoutePointsController : CrudController<TripRoutePoint, Guid>
{
    public TripRoutePointsController(IRepository<TripRoutePoint, Guid> repository) : base(repository)
    {
    }
}