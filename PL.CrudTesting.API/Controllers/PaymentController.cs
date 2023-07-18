using BLL.Entities.Payment;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class PaymentController : CrudController<Payment, Guid>
{
    public PaymentController(IRepository<Payment, Guid> repository) : base(repository)
    {
    }
}