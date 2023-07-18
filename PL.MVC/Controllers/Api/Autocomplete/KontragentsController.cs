using System.Linq.Expressions;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models.Autocomplete;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class KontragentsController : BaseAutocompleteController<BLL.Entities.KontragentIdentity.KontragentIdentity, PL.Entities.KontragentIdentity.KontragentIdentity, KontragentsSearchModel>
{
    public KontragentsController(IRepository<BLL.Entities.KontragentIdentity.KontragentIdentity, Guid> repository) : base(repository)
    {
    }

    protected override Expression<Func<BLL.Entities.KontragentIdentity.KontragentIdentity, bool>> GetFilterExpression(KontragentsSearchModel model)
    {
        return u => u.Discriminator == model.Discriminator.ToString() && u.Title.Contains(model.SearchQuery);
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.KontragentIdentity.KontragentIdentity>>> GetAsync([FromQuery] KontragentsSearchModel model)
    {
        return await base.GetAsync(model);
    }
}
