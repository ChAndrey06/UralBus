using AutoMapper;
using BLL.Entities.TripRoute;
using BLL.Interfaces;

using Common.Models;
using PL.Mappings.MapperFactory;
using PL.Services.Admin.Models;

namespace PL.Services.Client;
using PLTripRoute=PL.Entities.TripRoute.TripRoute;

public class PopularDestinationsService
{
    private readonly IRepository<TripRoute, Guid> _repository;
    private readonly IMapper _mapper;


    public PopularDestinationsService(IRepository<TripRoute, Guid> repository)
    {
        _repository = repository;
        _mapper=MapperFactory.GetMapper();
    }
    
    public async Task<GetWrapper<IEnumerable<Entities.TripRoute.TripRoute>>> GetAsync(int limit)
    {
        var bllTripGetWrapper = await _repository.GetAsync(
            limit: limit,
            offset: 0,
            filter: r => r.PopularDestination,
            //	orderBy: routes => routes.OrderByDescending(r=>r.CreatedAt) ,
            includeProperties:  new string[] {"TripRoutePoints", "TripRoutePoints.RoutePoint",nameof(TripRoute.PopularDestinationPicture)}
        );

        var plTripGetWrapper = new GetWrapper<IEnumerable<PLTripRoute>>(
            _mapper.Map<IEnumerable<PLTripRoute>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;
    }
}