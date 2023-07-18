namespace DAL.Entities.PersonIdentity;

public class PersonIdentity : BaseEntity
{
    public string Discriminator { get; set; }

    public string? IdentificationDocumentNumber { get; set; }

    public string? IdentificationDocumentType { get; set; }

    public Guid UserId { get; set; }
    public User.User User { get; set; }
}