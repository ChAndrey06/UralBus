namespace DAL.Entities.Return;

public class Return:BaseEntity
{
    public Guid PaymentId { get; set; }
    
    public decimal Sum { get; set; }
    
    public string ReturnType { get; set; }
}