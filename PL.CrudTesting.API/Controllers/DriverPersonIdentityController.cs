using BLL.Entities.PersonIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class DriverPersonIdentityController : CrudController<DriverPersonIdentity, Guid>
{
    public DriverPersonIdentityController(IRepository<DriverPersonIdentity, Guid> repository) : base(repository)
    {
    }
}