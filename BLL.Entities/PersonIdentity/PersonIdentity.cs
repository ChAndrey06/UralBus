using Common.Attributes;

namespace BLL.Entities.PersonIdentity;

public class PersonIdentity : BaseEntity
{
    [CanPatch]
    public string FirstName { get; set; }
    
    [CanPatch]
    public string LastName { get; set; }
    
    [CanPatch]
    public string Patronymic { get; set; }
    
    public string Discriminator { get; set; }

    [CanPatch]
    public string? IdentificationDocumentNumber { get; set; }

    [CanPatch]
    public string? IdentificationDocumentType { get; set; }

    public Guid UserId { get; set; }
    
    public User.User User { get; set; }
    
    public string FullName => $"{User?.LastName} {User?.FirstName} {User?.Patronymic}";
}