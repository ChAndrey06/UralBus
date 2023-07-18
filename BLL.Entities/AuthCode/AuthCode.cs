using Common.Enums;

namespace BLL.Entities.AuthCode;

public class AuthCode:BaseEntity
{
    public string Code { get; set; }
    
    public Guid UserId { get; set; }
    
    public User.User User { get; set; }
    
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(3).AddMinutes(10);
    
    public string Type { get; set; }
    
    public bool Used { get; set; } = false;
}