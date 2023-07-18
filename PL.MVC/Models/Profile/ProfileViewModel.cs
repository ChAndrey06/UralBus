namespace PL.MVC.Models.Profile;

public class ProfileViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    
    public string FullName => $"{LastName} {FirstName} {Patronymic}";
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}