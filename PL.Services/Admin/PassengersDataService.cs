using PLPassengerData = PL.Entities.PassengerData.PassengerData;
using BLLPassengerData = BLL.Entities.PassengerData.PassengerData;
using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;

namespace PL.Services.Admin;

public class PassengersDataService : BasePLAdminService<BLLPassengerData, PLPassengerData, FilterModel, Guid>
{
    public PassengersDataService(
        IRepository<BLLPassengerData, Guid> repository
    ) : base(repository)
    {
    }
    
    public override async Task<GetWrapper<IEnumerable<PLPassengerData>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";

        var bllPassengerDataGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter: r => (r.FirstName + r.LastName + r.Patronymic + r.Email + r.PhoneNumber).Contains(searchQuery),
            includeProperties: new [] { "User" }
        );
        
        var plPassengerDataGetWrapper = new GetWrapper<IEnumerable<PLPassengerData>>(
            _mapper.Map<IEnumerable<PLPassengerData>>(bllPassengerDataGetWrapper.Items),
            bllPassengerDataGetWrapper.TotalCount
        );

        return plPassengerDataGetWrapper;
    }

    public override async Task<PLPassengerData?> GetByIdAsync(Guid id)
    {
        var bllPassengerData = await _repository.GetByIdAsync(id, includeProperties: new [] { "User" });

        return _mapper.Map<PLPassengerData>(bllPassengerData);
    }
}