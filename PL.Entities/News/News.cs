using PL.Entities.File;

namespace PL.Entities.News;

public class News : BaseEntity
{
    private static int ShortDescriptionLength = 200;
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get
        {
            return Description.Length <= ShortDescriptionLength ? Description : Description.Substring(0, ShortDescriptionLength) + "...";
        }
    }
    public Guid? FileId { get; set; }
    public S3File? File { get; set; }
    public string? FilePath { get; set; }
}