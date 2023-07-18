namespace DAL.Entities.Files;

public class File : BaseEntity
{
	public string OriginalName { get; set; }

	public string ContentType { get; set; }

	public Guid UploaderId { get; set; }

	public User.User Uploader { get; set; }

	public string DocumentType { get; set; }

	public string Discriminator { get; set; }

	// public Guid? AttachedEntityId { get; set; }
}

public class Base64File : File
{
	public string Base64Content { get; set; }
}

public class S3File : File
{
	public string Path { get; set; }
}