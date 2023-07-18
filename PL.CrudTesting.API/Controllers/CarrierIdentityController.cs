using BLL.Entities.KontragentIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class CarrierIdentityController : CrudController<CarrierIdentity, Guid>
{
    public CarrierIdentityController(IRepository<CarrierIdentity, Guid> repository) : base(repository)
    {
    }
}