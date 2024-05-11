using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.Domain.Interfaces;

public interface IAccountService
{
    public Task<ApiResult<IEnumerable<AccountResponseDto>>> GetAllAsync();
    public Task<ApiResult<Account>> PostRegister(AccountResponseDto account);
    public Task<ApiResult<Account>> LoginUser(AccountLoginRequestDto account);

    public Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto account);

    public Task<Tasks> DeleteTask(int id);
}
