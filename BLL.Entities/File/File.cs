namespace BLL.Entities.File;

public class File : BaseEntity
{
	public string OriginalName { get; set; }

	public string ContentType { get; set; }

	public Guid UploaderId { get; set; }

	public User.User Uploader { get; set; }

	public string DocumentType { get; set; }

	public string Discriminator { get; set; }
}
