using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Task;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : Controller
{
	private readonly ITaskServices app;

	public TaskController(ITaskServices app)
	{
		this.app = app;
	}

	[HttpDelete]
	[Route("DeleteTask")]
	[Produces("application/json")]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> DeleteTask([FromBody] TaskRequestDto task)
	{
		var response = await this.app.DeleteTaskOfAccount(task.id);
		return StatusCode(200, response);
	}
}
