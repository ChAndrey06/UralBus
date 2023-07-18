using BLL.Entities.PersonIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class ClientPersonIdentityController : CrudController<ClientPersonIdentity, Guid>
{
    public ClientPersonIdentityController(IRepository<ClientPersonIdentity, Guid> repository) : base(repository)
    {
    }
}