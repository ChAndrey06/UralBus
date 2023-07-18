using BLL.Entities.TripDraft;
using BLL.Interfaces;
using Common.Models;
using PL.Services.Admin.Models;
using System.Linq;
namespace PL.Services.Admin;

public class TripDraftsService : BasePLAdminService<TripDraft, PL.Entities.TripDraft.TripDraft, TripDraftsFilterModel, Guid>
{
    public override async Task<GetWrapper<IEnumerable<PL.Entities.TripDraft.TripDraft>>> GetAsync(TripDraftsFilterModel filter)
    {
        var bllTripGetWrapper = await _repository
            .GetAsync(includeProperties: new[]
                {
                    "TripRoute",
                    "Carrier",
                    "Driver",
                    "Transport",
                    "TripRoute.TripRoutePoints",
                    "Driver.User"
                },
                filter: r =>
                    (filter.StartDayOfWeek == null || r.StartDayOfWeek == filter.StartDayOfWeek) &&
                    (filter.EndDayOfWeek == null || r.EndDayOfWeek == filter.EndDayOfWeek) &&
                    (filter.TripRouteId == null || r.TripRouteId == filter.TripRouteId)
            );


        var plTripGetWrapper = new GetWrapper<IEnumerable<PL.Entities.TripDraft.TripDraft>>(
            _mapper.Map<IEnumerable<PL.Entities.TripDraft.TripDraft>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;
    }

    public async Task<GetWrapper<IEnumerable<PL.Entities.TripDraft.TripDraft>>> GetByExistingKeys(
        IEnumerable<Guid> keys)
    {
        var bllTripGetWrapper = await _repository
            .GetAsync(includeProperties: new[]
                {
                    "TripRoute",
                    "Carrier",
                    "Driver",
                    "Transport",
                    "TripRoute.TripRoutePoints",
                    "Driver.User"
                },
                filter: r =>
                    keys.Contains(r.Id)
            );


        var plTripGetWrapper = new GetWrapper<IEnumerable<PL.Entities.TripDraft.TripDraft>>(
            _mapper.Map<IEnumerable<PL.Entities.TripDraft.TripDraft>>(bllTripGetWrapper.Items),
            bllTripGetWrapper.TotalCount
        );

        return plTripGetWrapper;
    }


    public override async Task<PL.Entities.TripDraft.TripDraft?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id, includeProperties: new[]
        {
            "TripRoute",
            "Carrier",
            "Driver",
            "Transport",
            "TripRoute.TripRoutePoints",
            "Driver.User"
        });

        return _mapper.Map<PL.Entities.TripDraft.TripDraft>(item);
    }

    public TripDraftsService(IRepository<TripDraft, Guid> repository) : base(repository)
    {
    }
}