using System.ComponentModel.DataAnnotations;

namespace PL.MVC.Models.Auth;

public class SmsAuthViewModel
{
    [Required(ErrorMessage = "не указан номер телефона")]
    [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Please enter a valid phone number.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "не указан код")]
    public string Code { get; set; }
}