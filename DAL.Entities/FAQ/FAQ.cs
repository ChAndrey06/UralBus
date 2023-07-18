
namespace DAL.Entities.FAQ;

public class FAQ : BaseEntity
{
	public string Question { get; set; }
	public string Answer { get; set; }
	public Guid CategoryId { get; set; }
	public FAQCategory Category { get; set; }
}


