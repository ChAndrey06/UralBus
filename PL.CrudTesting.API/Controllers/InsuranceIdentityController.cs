using BLL.Entities.KontragentIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class InsuranceIdentityController : CrudController<InsuranceIdentity, Guid>
{
    public InsuranceIdentityController(IRepository<InsuranceIdentity, Guid> repository) : base(repository)
    {
    }
}