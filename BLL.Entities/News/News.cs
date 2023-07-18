
using BLL.Entities.File;

namespace BLL.Entities.News;

public class News : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
	public Guid? FileId { get; set; }
	public S3File? File { get; set; }
}