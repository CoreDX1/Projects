using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.Domain.Interfaces;

public interface IAccountService
{
    public Task<IEnumerable<Account>> GetAllAsync();
    public Task<Account> PostRegister(AccountDto account);
    public Task<Account> LoginUser(AccountLoginRequestDto account);
    public Task<IEnumerable<TaskReponseDto>> GetTasksByAccount(AccountLoginRequestDto account);
    public Task<Tasks> DeleteTask(int id);
}
