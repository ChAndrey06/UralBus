using BLL.Entities.KontragentIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class KontragentIdentityController : CrudController<KontragentIdentity, Guid>
{
    public KontragentIdentityController(IRepository<KontragentIdentity, Guid> repository) : base(repository)
    {
    }
}