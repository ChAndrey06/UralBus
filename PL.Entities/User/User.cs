using Common.Enums;
using Newtonsoft.Json;
using PL.Entities.PersonIdentity;
namespace PL.Entities.User;

public class User : BaseEntity
{
    public ClientPersonIdentity? ClientIdentity { get; set; }

    private string? _password;
    public string? Password
    {
        get { return _password; }
        set
        {
            _password = value;
            PasswordHash = string.IsNullOrWhiteSpace(value)?null:BCrypt.Net.BCrypt.HashPassword(value);
        }
    }

    public string? PasswordHash { get; private set; }

    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public UserRole Roles
    {
        get
        {
            return EnumExtensions.ParseFlags<string, UserRole>(UserRolesString);
        }
        set
        {
            UserRolesString = value.ConvertFlagsToString();
        }
    }
    
    [JsonIgnore]
    public string UserRolesString { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Patronymic { get; set; }
    public string BirthDateString 
    { 
        get => BirthDate.HasValue ? BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty;
        set 
        {
            BirthDate = !string.IsNullOrEmpty(value)? DateTime.ParseExact(value, "dd/MM/yyyy", null):null;
        }
    }

    public DateTime? BirthDate { get; set; }

    public string? Sex { get; set; }

    public IdentificationDocumentType? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public bool? SendAdvertisements { get; set; }

    public ICollection<PersonIdentity.PersonIdentity> Identities { get; set; }
    
    public string FullName => $"{LastName} {FirstName} {Patronymic}";
}