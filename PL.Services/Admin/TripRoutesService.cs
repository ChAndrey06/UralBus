using BLL.Entities.TripRoute;

using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;
using PLTripRoute=PL.Entities.TripRoute.TripRoute;
namespace PL.Services.Admin
{
	public class TripRoutesService : BasePLAdminService<TripRoute, PLTripRoute, FilterModel, Guid>
	{
		public TripRoutesService(IRepository<TripRoute, Guid> repository) : base(repository)
		{
		}

		public override async Task<GetWrapper<IEnumerable<PLTripRoute>>> GetAsync(FilterModel filter)
		{
			var bllTripGetWrapper = await _repository.GetAsync(
				limit: filter.Limit,
				offset: filter.Offset,
				filter: r => string.IsNullOrEmpty(filter.SearchQuery) || r.Title.Contains(filter.SearchQuery),
			//	orderBy: routes => routes.OrderByDescending(r=>r.CreatedAt) ,
				includeProperties:  new string[] {"TripRoutePoints", "TripRoutePoints.RoutePoint"}
			);

			var plTripGetWrapper = new GetWrapper<IEnumerable<PLTripRoute>>(
				_mapper.Map<IEnumerable<PLTripRoute>>(bllTripGetWrapper.Items),
				bllTripGetWrapper.TotalCount
			);

			return plTripGetWrapper;
		}

        public async Task<GetWrapper<IEnumerable<PLTripRoute>>> GetAsync()
        {
            var bllTripGetWrapper = await _repository.GetAsync(
				includeProperties: new string[] { "TripRoutePoints", "TripRoutePoints.RoutePoint" }
            );

            var plTripGetWrapper = new GetWrapper<IEnumerable<PLTripRoute>>(
                _mapper.Map<IEnumerable<PLTripRoute>>(bllTripGetWrapper.Items),
                bllTripGetWrapper.TotalCount
            );

            return plTripGetWrapper;
        }

        public override async Task<PLTripRoute?> GetByIdAsync(Guid id)
		{
			var bllTrip = await _repository.GetByIdAsync(id, includeProperties: new[] { "TripRoutePoints", "TripRoutePoints.RoutePoint", "PopularDestinationPicture" });

			return _mapper.Map<PLTripRoute>(bllTrip);
		}
	}
}
