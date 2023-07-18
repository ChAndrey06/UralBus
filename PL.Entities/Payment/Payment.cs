namespace PL.Entities.Payment;

public class Payment:BaseEntity
{
    
    public string Status { get; set; }
    
    
    public decimal Sum { get; set; }
    
    public Return.Return? Return { get; set; }
    
    public Guid? ReturnId { get; set; }
}