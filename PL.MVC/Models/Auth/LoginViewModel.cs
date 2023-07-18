using System.ComponentModel.DataAnnotations;

namespace PL.MVC.Models.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан Email/Номер телефона")]
    public string Login { get; set; } // email or phone number
    
    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
}