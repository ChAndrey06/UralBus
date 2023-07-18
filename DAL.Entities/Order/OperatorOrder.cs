using DAL.Entities.PersonIdentity;

namespace DAL.Entities.Order;

public class OperatorOrder:Order
{
    public OperatorPersonIdentity Operator { get; set; }
   
    public Guid OperatorId { get; set; }
}