using Api.Data;
using Api.Models.Dto.Account.Request;
using Api.Models.Dto.Account.Response.Task;
using Api.Models.Entities;
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
        Account newAccount =
            new()
            {
                Email = account.Email,
                Password = account.Password,
                UserName = account.UserName
            };

        await _db.Accounts.AddAsync(newAccount);
        await _db.SaveChangesAsync();

        return newAccount;
    }

    public async Task<Account> LoginUser(AccountDto account)
    {
        var response = await _db.Accounts.FirstAsync(x =>
            x.Email == account.Email && x.Password == account.Password
        );
        return response;
    }

    public async Task<IEnumerable<TaskReponseDto>> GetTasksByAccount(AccountDto account)
    {
        var loggedInAccount = await LoginUser(account);
        var userTask = await _db.Tasks.Where(x => x.UserId == loggedInAccount.UserId).ToListAsync();

        var taskReponseDtoList = userTask.Select(task => new TaskReponseDto
        {
            Title = task.Title,
            Completed = task.Completed,
            Description = task.Description
        });

        return taskReponseDtoList;
    }
}
