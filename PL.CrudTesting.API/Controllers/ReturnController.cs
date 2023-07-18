using BLL.Entities.Return;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class ReturnController : CrudController<Return, Guid>
{
    public ReturnController(IRepository<Return, Guid> repository) : base(repository)
    {
    }
}