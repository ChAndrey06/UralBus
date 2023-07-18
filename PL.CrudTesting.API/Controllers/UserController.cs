using BLL.Entities.User;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.CrudTesting.API.Controllers;

public class UserController : CrudController<User,Guid>
{
    public UserController(IRepository<User, Guid> repository) : base(repository)
    {
    }

    [HttpGet(nameof(CheckPhoneExist))]
    public async Task<ActionResult<bool>> CheckPhoneExist(string phoneNumber)
    {
        var user = await _repository.GetAsync(filter: u => u.PhoneNumber == phoneNumber);

        return Ok(user.TotalCount != 0);
    }

    [HttpGet(nameof(CheckEmailExist))]
    public async Task<ActionResult<bool>> CheckEmailExist(string email)
    {
        var user = await _repository.GetAsync(filter: u => u.Email == email);

        return Ok(user.TotalCount != 0);
    }
}

