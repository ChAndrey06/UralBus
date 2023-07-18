using Common.Enums;

namespace PL.MVC.Models.User;
public class UserModel
{
	public Guid Id { get; set; }
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; } = DateTime.UtcNow.AddHours(3);
    public string Sex { get; set; } = Common.Enums.Sex.Male.ToString();
    public string Roles { get; set; } = string.Empty;
}	