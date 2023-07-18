using PLTransport = PL.Entities.Transport.Transport;
using BLLTransport = BLL.Entities.Transport.Transport;
using PL.Services.Admin.Models;
using Common.Models;
using BLL.Interfaces;
using PL.Entities.Transport;

namespace PL.Services.Admin;

public class TransportsService : BasePLAdminService<BLLTransport, PLTransport, FilterModel, Guid>
{
    public TransportsService(IRepository<BLLTransport, Guid> repository) : base(repository)
    {
    }

    public override async Task<GetWrapper<IEnumerable<PLTransport>>> GetAsync(FilterModel filter)
    {
        var searchQuery = filter.SearchQuery ?? "";

        var bllFAQGetWrapper = await _repository.GetAsync(
            limit: filter.Limit,
            offset: filter.Offset,
            filter:r=>(filter.ExistKeys==null||filter.ExistKeys.Count==0||filter.ExistKeys.Contains(r.Id)),
            includeProperties: new[]
            {
                nameof(Transport.Carrier),
                nameof(Transport.Driver),
                $"{nameof(Transport.Driver)}.{nameof(Transport.Driver.User)}"
            }
        );

        var plFAQGetWrapper = new GetWrapper<IEnumerable<PLTransport>>(
            _mapper.Map<IEnumerable<PLTransport>>(bllFAQGetWrapper.Items),
            bllFAQGetWrapper.TotalCount
        );

        return plFAQGetWrapper;
    }
    

    public override async Task<PLTransport?> GetByIdAsync(Guid id)
    {
        var bllFAQ = await _repository.GetByIdAsync(
            id, 
            includeProperties: new[] { "Carrier", "Driver", "Driver.User" }
        );

        return _mapper.Map<PLTransport>(bllFAQ);
    }
}
