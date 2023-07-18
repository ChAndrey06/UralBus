using BLL.Entities.PersonIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class DispatcherPersonIdentityController : CrudController<DispatcherPersonIdentity, Guid>
{
    public DispatcherPersonIdentityController(IRepository<DispatcherPersonIdentity, Guid> repository) : base(repository)
    {
    }
}