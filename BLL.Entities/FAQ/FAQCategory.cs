namespace BLL.Entities.FAQ;

public class FAQCategory : BaseEntity
{
	public string Title { get; set; }
	public ICollection<FAQ> FAQs { get; set; }
}