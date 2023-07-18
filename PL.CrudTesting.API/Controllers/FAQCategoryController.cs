using BLL.Entities.FAQ;
using BLL.Interfaces;

namespace PL.CrudTesting.API.Controllers;

public class FAQCategoryController : CrudController<FAQCategory, Guid>
{
	public FAQCategoryController(IRepository<FAQCategory, Guid> repository) : base(repository)
	{
	}
}
