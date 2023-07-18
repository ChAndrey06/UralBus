using Common.Enums;

namespace PL.MVC.Models.Autocomplete;

public class KontragentsSearchModel : BaseAutocompleteSearchModel
{
    public KontragentIdentityType Discriminator { get; set; }
}
