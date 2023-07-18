using System.Linq.Expressions;
using BLL.Entities.Transport;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class TransportsController : BaseAutocompleteController<BLL.Entities.Transport.Transport, PL.Entities.Transport.Transport, BaseAutocompleteSearchModel>
{
    public TransportsController(IRepository<Transport, Guid> repository) : base(repository)
    {
    }
    
    protected override Expression<Func<BLL.Entities.Transport.Transport, bool>> GetFilterExpression(BaseAutocompleteSearchModel model)
    {
        return r => r.Title.Contains(model.SearchQuery);
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.Transport.Transport>>> GetAsync([FromQuery] BaseAutocompleteSearchModel model)
    {
        return await base.GetAsync(model);
    }
}