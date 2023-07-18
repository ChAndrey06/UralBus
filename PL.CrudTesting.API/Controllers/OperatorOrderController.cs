using BLL.Entities.Order;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class OperatorOrderController : CrudController<OperatorOrder, Guid>
{
    public OperatorOrderController(IRepository<OperatorOrder, Guid> repository) : base(repository)
    {
    }
}