using BLL.Entities.FAQ;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class FAQController : CrudController<FAQ, Guid>
{
	public FAQController(IRepository<FAQ, Guid> repository) : base(repository)
	{
	}
}
