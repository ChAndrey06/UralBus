using System.Text.Json.Serialization;
using Common.Helpers;

namespace PL.Services.Admin.Models;

public class FilterModel : IFilterModel
{
	public int Page { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public int Limit => Page * PageSize;
	public int Offset => (Page - 1) * PageSize;
	public string? SearchQuery { get; set; }
	
	public List<Guid>? ExistKeys { get; set; }

    [JsonIgnore]
    public OrderByModel? OrderByModel => !string.IsNullOrWhiteSpace(OrderByQuery) ? new OrderByModel()
    {
        PropertyName = OrderByQuery.Split(".").First(),
        Direction = OrderByQuery.Split(".").Last().ParseEnum<OrderByDirection>()
    } : null;

    public string? OrderByQuery { get; set; }
}

public class OrderByModel
{
	public string PropertyName { get; set; }
	
	public OrderByDirection Direction { get; set; }
}
public enum OrderByDirection
{
    Asc,
    Desc
}
public interface IFilterModel
{
	public int Page { get; set; }
	public int PageSize { get; set; }

	public int Limit { get; }
	public int Offset { get; }
	public string SearchQuery { get; set; }
}