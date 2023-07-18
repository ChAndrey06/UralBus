using Common.Models;

namespace PL.MVC.Models;

public class SearchResultViewModel<TModel, TFilterModel> where TFilterModel : class, new()
{
	public GetWrapper<IEnumerable<TModel>> Objects { get; set; }
	public TFilterModel Filter { get; set; } = new TFilterModel();

	public SearchResultViewModel(GetWrapper<IEnumerable<TModel>> objects, TFilterModel filter)
	{
		Filter = filter;
		Objects = objects;
	}
}