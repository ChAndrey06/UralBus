using System.ComponentModel.DataAnnotations;

namespace PL.MVC.Models.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Не указан Email")]
    public string Email { get; set; }
    
    
    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Не указан номер телефона")]
    public string PhoneNumber { get; set; }
}