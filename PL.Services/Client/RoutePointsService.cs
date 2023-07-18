using AutoMapper;
using BLL.Interfaces;
using Common.Models;
using PL.Mappings.MapperFactory;
using PL.Services.Admin.Models;
using Constants = Common.Configuration.Constants;
using BLLRoutePoint = BLL.Entities.RoutePoint.RoutePoint;
using PLRoutePoint = PL.Entities.RoutePoint.RoutePoint;

namespace PL.Services.Client;

public class RoutePointsService
{
	protected readonly IMapper _mapper;
	protected readonly IRepository<BLLRoutePoint, Guid> _repository;

	public RoutePointsService(IRepository<BLLRoutePoint, Guid> repository)
	{
		_repository = repository;
		_mapper = MapperFactory.GetMapper();
	}

	public async Task<PLRoutePoint> GetByIdAsync(Guid id)
	{
		return _mapper.Map<PLRoutePoint>(await _repository.GetByIdAsync(id));
	}

	public async Task<IEnumerable<PLRoutePoint>> GetBasicPoints()
	{
		var items = await _repository.GetAsync(filter: r => r.Id == Constants.BasicFromId || r.Id == Constants.BasicToId);

		if (items.TotalCount < 2)
		{
			throw new Exception("Basic points not defined");
		}

		var from = items.Items.FirstOrDefault(r => r.Id == Constants.BasicFromId);
		var to = items.Items.FirstOrDefault(r => r.Id == Constants.BasicToId);

		return new[] { _mapper.Map<PLRoutePoint>(from), _mapper.Map<PLRoutePoint>(to) };
	}

	public async Task<GetWrapper<IEnumerable<PLRoutePoint>>> GetAsync(FilterModel filter)
	{
		var bllPLRoutePointGetWrapper = await _repository.GetAsync(
			filter: r => filter.SearchQuery == null || r.Title.Contains(filter.SearchQuery),
			limit: filter.Limit
		);

		var plPLRoutePointGetWrapper = new GetWrapper<IEnumerable<PLRoutePoint>>(
			_mapper.Map<IEnumerable<PLRoutePoint>>(bllPLRoutePointGetWrapper.Items),
			bllPLRoutePointGetWrapper.TotalCount
		);

		return plPLRoutePointGetWrapper;
	}
}
