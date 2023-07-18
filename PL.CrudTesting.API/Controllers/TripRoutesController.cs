using BLL.Entities.TripRoute;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class TripRoutesController : CrudController<TripRoute, Guid>
{
    public TripRoutesController(IRepository<TripRoute, Guid> repository) : base(repository)
    {
    }
}