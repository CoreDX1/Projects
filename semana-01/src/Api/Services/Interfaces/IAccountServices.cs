using Api.Models.Entities;
using Api.Models.Dto.Account.Request;


namespace Api.Services.Interfaces;

public interface IAccountService
{
    public Task<IEnumerable<Account>> GetAll();
    public Task<Account> PostRegister(AccountDto account);
}
