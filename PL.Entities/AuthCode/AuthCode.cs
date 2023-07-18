using Common.Enums;
using Common.Helpers;
using System.Text.Json.Serialization;

namespace PL.Entities.AuthCode;

public class AuthCode : BaseEntity
{
    public string Code { get; set; }
    
    public Guid UserId { get; set; }
    
    public User.User User { get; set; }
    
    public DateTime ExpiresAt { get; set; }= DateTime.UtcNow.AddHours(3).AddMinutes(10);
    

    [JsonIgnore]
    public string TypeString { get; set; }

    public AuthCodeType Type
    {
        get
        {
            //return TypeString.ParseEnum<AuthCodeType>();
            return AuthCodeType.Authorization;
		}
        set
        {
            TypeString = value.ToString();
        }
    }

    public bool Used { get; set; } = false;
}