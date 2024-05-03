using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.Domain.Interfaces;

public interface IAccountService
{
    public Task<ApiResponse<IEnumerable<Account>>> GetAllAsync();
    public Task<ApiResponse<Account>> PostRegister(AccountDto account);
    public Task<ApiResponse<Account>> LoginUser(AccountLoginRequestDto account);
    public Task<LoginResponse> GetTasksByAccount(AccountLoginRequestDto account);
    public Task<Tasks> DeleteTask(int id);
}
