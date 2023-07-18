using BLL.Entities.Trip;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class TripsController : CrudController<Trip, Guid>
{
    public TripsController(IRepository<Trip, Guid> repository) : base(repository)
    {
    }
}