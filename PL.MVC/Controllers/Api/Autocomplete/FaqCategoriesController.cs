using System.Linq.Expressions;
using BLL.Entities.FAQ;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class FaqCategoriesController : BaseAutocompleteController<BLL.Entities.FAQ.FAQCategory, PL.Entities.FAQ.FAQCategory, BaseAutocompleteSearchModel>
{
    public FaqCategoriesController(IRepository<FAQCategory, Guid> repository) : base(repository)
    {
    }
    
    protected override Expression<Func<BLL.Entities.FAQ.FAQCategory, bool>> GetFilterExpression(BaseAutocompleteSearchModel model)
    {
        return r => r.Title.Contains(model.SearchQuery);
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.FAQ.FAQCategory>>> GetAsync([FromQuery] BaseAutocompleteSearchModel model)
    {
        return await base.GetAsync(model);
    }
}