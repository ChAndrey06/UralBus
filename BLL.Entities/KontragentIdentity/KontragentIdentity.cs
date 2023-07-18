using Common.Attributes;

namespace BLL.Entities.KontragentIdentity;

public class KontragentIdentity : BaseEntity
{
    [CanPatch]
    public string Title { get; set; }

    [CanPatch]
    public string Discriminator { get; set; }

    [CanPatch]
    public string ContractPhone { get; set; }

    [CanPatch]
    public string ContractEmail { get; set; }
}