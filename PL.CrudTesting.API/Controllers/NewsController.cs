using BLL.Interfaces;
using BLL.Entities.News;

namespace PL.CrudTesting.API.Controllers;

public class NewsController : CrudController<News, Guid>
{
	public NewsController(IRepository<News, Guid> repository) : base(repository)
	{
	}
}