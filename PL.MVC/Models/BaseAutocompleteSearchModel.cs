using System.Diagnostics.CodeAnalysis;

namespace PL.MVC.Models;

public class BaseAutocompleteSearchModel
{
    public int Limit { get; set; } = 20;

    private string _searchQuery = string.Empty;
    [AllowNull]
    public string SearchQuery { 
        get 
        { 
            return _searchQuery; 
        } 
        set 
        { 
            _searchQuery = value ?? string.Empty;
        } 
    }
}
