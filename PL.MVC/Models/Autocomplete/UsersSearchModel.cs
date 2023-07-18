using Common.Enums;

namespace PL.MVC.Models.Autocomplete;

public class UsersSearchModel : BaseAutocompleteSearchModel
{
    public UserRole Role { get; set; }
}