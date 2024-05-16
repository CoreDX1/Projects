using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
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
	[HttpGet("Accounts")] // GET: api/account
	[Produces("application/json")]
	[ProducesDefaultResponseType]
	[ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
	public async Task<IActionResult> GetAccountAsync()
	{
		var accounts = await _app.GetAllAsync();
		return StatusCode(200, accounts);
	}

	[HttpPost("Register")] // POST: api/account/register
	[Produces("application/json")]
	[ProducesDefaultResponseType]
	[ProducesResponseType(200, Type = typeof(Account))]
	[ProducesResponseType(400)]
	public async Task<ActionResult> AddAccount([FromBody] AccountResponseDto account)
	{
		var response = await _app.PostRegister(account);
		return StatusCode(200, response);
	}

	[HttpPost("Login")] // POST: api/account/login
	[Produces("application/json")]
	[ProducesDefaultResponseType]
	[ProducesResponseType(200, Type = typeof(Account))]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostLogin([FromBody] AccountLoginRequestDto account)
	{
		var response = await _app.LoginUser(account);
		return StatusCode(200, response);
	}

	[HttpPost("GetTasks")] // POST: api/account/gettasks
	[Produces("application/json")]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> GetTasks([FromBody] AccountLoginRequestDto account)
	{
		var response = await _app.GetTasksForAccount(account);
		return Ok(response);
	}
}
