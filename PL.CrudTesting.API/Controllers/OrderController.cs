using BLL.Entities.Order;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class OrderController : CrudController<Order, Guid>
{
    public OrderController(IRepository<Order, Guid> repository) : base(repository)
    {
    }
}