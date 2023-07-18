using System.Linq.Expressions;
using BLL.Entities.PersonIdentity;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class DriversController : BaseAutocompleteController<BLL.Entities.PersonIdentity.DriverPersonIdentity, PL.Entities.PersonIdentity.DriverPersonIdentity, BaseAutocompleteSearchModel>
{
    public DriversController(IRepository<DriverPersonIdentity, Guid> repository) : base(repository)
    {
        IncludeProperties = new[] { "User" };
    }
    
    protected override Expression<Func<BLL.Entities.PersonIdentity.DriverPersonIdentity, bool>> GetFilterExpression(BaseAutocompleteSearchModel model)
    {
        return r => (r.User.FirstName + r.User.LastName + r.User.Patronymic).Contains(model.SearchQuery);
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.PersonIdentity.DriverPersonIdentity>>> GetAsync([FromQuery] BaseAutocompleteSearchModel model)
    {
        return await base.GetAsync(model);
    }
}