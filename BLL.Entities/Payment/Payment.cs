using Common.Attributes;

namespace BLL.Entities.Payment;

public class Payment:BaseEntity
{
    [CanPatch]
    public string Status { get; set; }
    
    [CanPatch]
    public decimal Sum { get; set; }
    
    public Return.Return? Return { get; set; }
    
    public Guid? ReturnId { get; set; }
}