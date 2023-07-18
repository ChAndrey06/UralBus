using Common.Enums;
using Common.Helpers;
using Newtonsoft.Json;

namespace PL.Entities.PersonIdentity;

public class PersonIdentity : BaseEntity
{
    public Guid UserId { get; set; }
    
    public User.User User { get; set; }
    
    public PersonIdentityType IdentityType { get; set; }

    public string IdentificationDocumentNumber { get; set; }

    [JsonIgnore]
    public string IdentificationDocumentTypeString { get; set; }

    public IdentificationDocumentType? IdentificationDocumentType
    {
        get
        {
            return !string.IsNullOrWhiteSpace(IdentificationDocumentTypeString) ? IdentificationDocumentTypeString.ParseEnum<IdentificationDocumentType>() : null;
        }
        set
        {
            IdentificationDocumentTypeString = value.ToString();
        }
    }
    
    public string FullName => $"{User?.LastName} {User?.FirstName} {User?.Patronymic}";
}