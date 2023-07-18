using System.Linq.Expressions;
using BLL.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models.Autocomplete;

namespace PL.MVC.Controllers.Api.Autocomplete;

[Route("autocomplete/[controller]")]
[ApiController]
public class UsersController : BaseAutocompleteController<BLL.Entities.User.User, PL.Entities.User.User, UsersSearchModel>
{
    public UsersController(IRepository<BLL.Entities.User.User, Guid> repository) : base(repository)
    {
    }

    protected override Expression<Func<BLL.Entities.User.User, bool>> GetFilterExpression(UsersSearchModel model)
    {
        // return r => (r.Email + r.PhoneNumber + r.FirstName).Contains(model.SearchQuery) && r.Roles.ToString().Contains(model.Role.ToString());
        return r => (r.Email + r.PhoneNumber + r.FirstName).Contains(model.SearchQuery) && r.Roles.Contains(model.Role.ToString());
    }

    [HttpGet]
    public override async Task<GetWrapper<IEnumerable<PL.Entities.User.User>>> GetAsync([FromQuery] UsersSearchModel model)
    {
        return await base.GetAsync(model);
    }
}
