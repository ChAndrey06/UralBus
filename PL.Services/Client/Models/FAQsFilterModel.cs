using PL.Services.Admin.Models;

namespace PL.Services.Client.Models;

public class FAQsFilterModel : FilterModel
{
	public Guid CategoryId { get; set; }
}
