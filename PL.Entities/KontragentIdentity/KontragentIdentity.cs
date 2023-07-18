namespace PL.Entities.KontragentIdentity;

public class KontragentIdentity : BaseEntity
{
    public string Title { get; set; }

    public string Discriminator { get; set; }

	public string ContractPhone { get; set; }

	public string ContractEmail { get; set; }
}