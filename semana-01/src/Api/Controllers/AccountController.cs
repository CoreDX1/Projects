using Api.Models.Dto.Account.Request;
using Api.Models.Entities;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _app;

    public AccountController(IAccountService app)
    {
        _app = app;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IEnumerable<Account>> GetAll()
    {
        var response = await _app.GetAll();
        return response;
    }

    [HttpPost]
    public async Task<ActionResult> AddAccount([FromBody] AccountDto account)
    {
        var response = await _app.PostRegister(account);
        return Ok(response);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<Account>> GetAccount([FromBody] AccountDto account)
    {
        var response = await _app.LoginUser(account);
        return Ok(response);
    }

    [HttpPost]
    [Route("GetTasks")]
    public async Task<ActionResult> GetTasks([FromBody] AccountDto account)
    {
        var response = await _app.GetTasksByAccount(account);
        return Ok(response);
    }
}
