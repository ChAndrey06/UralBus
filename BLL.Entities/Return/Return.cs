using Common.Attributes;
using Common.Enums;

namespace BLL.Entities.Return;

public class Return:BaseEntity
{
    public Guid PaymentId { get; set; }
    
    [CanPatch]
    public decimal Sum { get; set; }
    
    [CanPatch]
    public string ReturnType { get; set; }
}