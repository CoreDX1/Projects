using Api.Models.Dto.Account.Request;
using Api.Models.Dto.Account.Response.Task;
using Api.Models.Entities;

namespace Api.Services.Interfaces;

public interface IAccountService
{
    public Task<IEnumerable<Account>> GetAll();
    public Task<Account> PostRegister(AccountDto account);
    public Task<Account> LoginUser(AccountDto account);
    public Task<IEnumerable<TaskReponseDto>> GetTasksByAccount(AccountDto account);
}
