using BLL.Entities.File;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class Base64FileController : CrudController<Base64File, Guid>
{
	public Base64FileController(IRepository<Base64File, Guid> repository) : base(repository)
	{
	}
}