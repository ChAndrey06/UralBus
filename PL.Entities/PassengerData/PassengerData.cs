namespace PL.Entities.PassengerData;

public class PassengerData : BaseEntity
{
    public Guid UserId { get; set; }
    
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime? BirthDate { get; set; }
    
    public User.User User { get; set; }
}