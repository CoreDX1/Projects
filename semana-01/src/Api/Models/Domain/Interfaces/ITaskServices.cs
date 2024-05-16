using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;


namespace Api.Models.Domain.Interfaces;

public interface ITaskServices
{
	public Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto account);
	public Task<ApiResult<Tasks>> DeleteTaskOfAccount(int id);
}