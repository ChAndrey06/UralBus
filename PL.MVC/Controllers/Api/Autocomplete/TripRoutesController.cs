using System.Linq.Expressions;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class TripRoutesController : BaseAutocompleteController<BLL.Entities.TripRoute.TripRoute, PL.Entities.TripRoute.TripRoute, BaseAutocompleteSearchModel>
{
    public TripRoutesController(IRepository<BLL.Entities.TripRoute.TripRoute, Guid> repository) : base(repository)
    {
    }
    
    protected override Expression<Func<BLL.Entities.TripRoute.TripRoute, bool>> GetFilterExpression(BaseAutocompleteSearchModel model)
    {
        return r => r.Title.Contains(model.SearchQuery);
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.TripRoute.TripRoute>>> GetAsync([FromQuery] BaseAutocompleteSearchModel model)
    {
        return await base.GetAsync(model);
    }
}