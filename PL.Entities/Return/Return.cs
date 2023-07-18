using Common.Enums;
using Common.Helpers;
using System.Text.Json.Serialization;

namespace PL.Entities.Return;

public class Return:BaseEntity
{
    public Guid PaymentId { get; set; }
    
    public decimal Sum { get; set; }
    
    [JsonIgnore]
    public string ReturnTypeString { get; set; }

    public ReturnType ReturnType
    {
        get
        {
            return ReturnTypeString.ParseEnum<ReturnType>();
        }
        set
        {
            ReturnTypeString = value.ToString();
        }
    }
}