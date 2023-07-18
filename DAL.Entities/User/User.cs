namespace DAL.Entities.User;

public class User : BaseEntity
{
    public string? PasswordHash { get; set;}
    
    public string? Email { get; set;}
    
    public string? PhoneNumber { get; set;}
    
    public string Roles { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public string? Sex { get; set; }
    
    public ICollection<AuthCode.AuthCode> AuthCodes { get; set; }

    public ICollection<PersonIdentity.PersonIdentity> Identities { get; set; }
}