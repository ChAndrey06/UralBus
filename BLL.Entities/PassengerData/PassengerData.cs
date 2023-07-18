using Common.Attributes;

namespace BLL.Entities.PassengerData;

public class PassengerData : BaseEntity
{
    public Guid UserId { get; set; }
    
    [CanPatch]
    public string? Email { get; set; }
    [CanPatch]
    public string? PhoneNumber { get; set; }
    [CanPatch]
    public string FirstName { get; set; }
    [CanPatch]
    public string LastName { get; set; }
    [CanPatch]
    public string Patronymic { get; set; }
    [CanPatch]
    public DateTime? BirthDate { get; set; }

    public User.User User { get; set; }
}