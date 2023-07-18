using Common.Attributes;
using Common.Enums;

namespace BLL.Entities.User;

public class User : BaseEntity
{
    public string? PasswordHash { get; set; }
    
    [CanPatch]
    public string? Email { get; set; }
    
    [CanPatch]
    public string? PhoneNumber { get; set; }
    
    public string Roles { get; set; }

    [CanPatch]
    public string? FirstName { get; set; }

    [CanPatch]
    public string? LastName { get; set; }

    [CanPatch]
    public string? Patronymic { get; set; }

    [CanPatch]
    public DateTime? BirthDate { get; set; }

    [CanPatch]
    public string? Sex { get; set; }

    public ICollection<PersonIdentity.PersonIdentity> Identities { get; set; }
}