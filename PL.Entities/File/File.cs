using Common.Enums;
using Common.Helpers;
using System.Text.Json.Serialization;

namespace PL.Entities.File;

public class File : BaseEntity
{
	public string OriginalName { get; set; }

	public string ContentType { get; set; }

	public Guid UploaderId { get; set; }

	public User.User Uploader { get; set; }

	[JsonIgnore]
	public string DocumentTypeString { get; set; }

	public DocumentType DocumentType
	{
		get
		{
			return DocumentTypeString.ParseEnum<DocumentType>();
		}
		set
		{
			DocumentTypeString = value.ToString();
		}
	}

	public string Discriminator { get; set; }
}
