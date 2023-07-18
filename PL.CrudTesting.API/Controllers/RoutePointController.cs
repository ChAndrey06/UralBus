using BLL.Entities.RoutePoint;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class RoutePointController : CrudController<RoutePoint, Guid>
{
    public RoutePointController(IRepository<RoutePoint, Guid> repository) : base(repository)
    {
    }
}