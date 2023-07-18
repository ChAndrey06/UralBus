using BLL.Entities.PersonIdentity;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class PersonIdentityController : CrudController<PersonIdentity, Guid>
{
    public PersonIdentityController(IRepository<PersonIdentity, Guid> repository) : base(repository)
    {
    }
}