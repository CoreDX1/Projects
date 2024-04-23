using Api.Data;
using Api.Models.Entities;
using Api.Models.Dto.Account.Request;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;


public class AccountService : IAccountService
{
    private Semana01Context _db;

    public AccountService(Semana01Context db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        var account = await _db.Accounts.AsNoTracking().ToListAsync();
        return account;
    }

    public async Task<Account> PostRegister(AccountDto account)
    {

        Account newAccount = new() { Email = account.Email, Password = account.Password, UserName = account.UserName };

        await _db.Accounts.AddAsync(newAccount);
        await _db.SaveChangesAsync();

        return newAccount;
    }
}
