using BLL.Entities.Transport;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class TransportController : CrudController<Transport, Guid>
{
    public TransportController(IRepository<Transport, Guid> repository) : base(repository)
    {
    }
}