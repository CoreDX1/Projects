using Api.Models.Dto.Account.Request;
using Api.Models.Dto.Account.Response.Task;
using Api.Models.Entities;

namespace Api.Services.Interfaces;

public interface IAccountService
{
    public Task<IEnumerable<Account>> GetAllAsync();
    public Task<Account> PostRegister(AccountDto account);
    public Task<Account> LoginUser(AccountLoginRequestDto account);
    public Task<IEnumerable<TaskReponseDto>> GetTasksByAccount(AccountLoginRequestDto account);
    public Task<Tasks> DeleteTask(int id);
}
