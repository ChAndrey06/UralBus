using BLL.Entities.PersonIdentity;

namespace BLL.Entities.Order;

public class OperatorOrder:Order
{
    public OperatorPersonIdentity Operator { get; set; }
    
    public Guid OperatorId { get; set; }
}