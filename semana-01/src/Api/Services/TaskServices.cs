using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
using Api.Utilities.Static;

namespace Api.services.Repository;

public class TaskServices : ITaskServices
{
	private readonly Semana01Context db;
	private readonly ITaskRepository taskRepository;

	public TaskServices(Semana01Context db, ITaskRepository taskRepository)
	{
		this.db = db;
		this.taskRepository = taskRepository;
	}

	public async Task<ApiResult<Tasks>> DeleteTaskOfAccount(int id)
	{
		var taskDelete = await this.taskRepository.DeleteTask(id);
		return ApiResult<Tasks>.Success(taskDelete, ReplyMessage.MESSAGE_DELETE, StatusCodes.Status200OK);
	}

	public Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto account)
	{
		throw new NotImplementedException();
	}
}
