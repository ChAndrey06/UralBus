using PL.Entities.PersonIdentity;

namespace PL.Entities.Order;

public class OperatorOrder:Order
{
    public OperatorPersonIdentity Operator { get; set; }
    
    public Guid OperatorId { get; set; }
}