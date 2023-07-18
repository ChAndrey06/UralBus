using BLL.Entities.PersonIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class OperatorPersonIdentityController : CrudController<OperatorPersonIdentity, Guid>
{
    public OperatorPersonIdentityController(IRepository<OperatorPersonIdentity, Guid> repository) : base(repository)
    {
    }
}