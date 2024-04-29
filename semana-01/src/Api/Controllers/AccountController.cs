using Api.Models.Dto.Account.Request;
using Api.Models.Dto.Account.Response.Task;
using Api.Models.Dto.Task.Request;
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

    /// <summary>
    /// Get all accounts
    /// </summary>
    /// <returns>List of accounts</returns>
    [HttpGet] // GET: api/Account
    [Route("accounts", Name = "GetAccount")]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
    public async Task<IActionResult> GetAccountAsync()
    {
        IEnumerable<Account> accounts = await _app.GetAllAsync();
        return StatusCode(200, accounts);
    }

    [HttpPost]
    [Route("Register")]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(200, Type = typeof(Account))]
    [ProducesResponseType(400)]
    public async Task<ActionResult> AddAccount([FromBody] AccountDto account)
    {
        var response = await _app.PostRegister(account);
        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("Login")]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(200, Type = typeof(Account))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostLogin([FromBody] AccountLoginRequestDto account)
    {
        var response = await _app.LoginUser(account);
        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("GetTasks")]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TaskReponseDto>))]
    public async Task<ActionResult> GetTasks([FromBody] AccountLoginRequestDto account)
    {
        var response = await _app.GetTasksByAccount(account);
        return Ok(response);
    }

    [HttpDelete]
    [Route("DeleteTask")]
    [Produces("application/json")]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteTask([FromBody] TaskRequestDto task)
    {
        var response = await _app.DeleteTask(task.id);
        return StatusCode(200, response);
    }

}
