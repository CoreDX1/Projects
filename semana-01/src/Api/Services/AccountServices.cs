using Api.Data;
using Api.Models.Dto.Account.Request;
using Api.Models.Dto.Account.Response.Task;
using Api.Models.Entities;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AccountService : IAccountService
{
    private readonly Semana01Context _db;
    private readonly IAccountRepository _accountRepository;
    private readonly ITaskRepository _taskRepository;

    public AccountService(Semana01Context db, IAccountRepository accountRepository, ITaskRepository taskRepository)
    {
        _db = db;
        _accountRepository = accountRepository;
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        var account = await _accountRepository.GetAllAsync();
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

        await _accountRepository.AddAsync(newAccount);

        return newAccount;
    }

    public async Task<Account> LoginUser(AccountLoginRequestDto account)
    {
        var response = await _accountRepository.GetByEmailAndPasswordAsync(account.Email, account.Password);
        return response;
    }

    public async Task<IEnumerable<TaskReponseDto>> GetTasksByAccount(AccountLoginRequestDto account)
    {

        var loggetInAccount = await LoginUser(account);

        var userTasks = await _taskRepository.GetByUserIdAsync(loggetInAccount.UserId);


        var taskReponseDtoList = userTasks.Select(task => new TaskReponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Completed = task.Completed,
            Description = task.Description
        });

        return taskReponseDtoList;
    }

    public async Task<Tasks> DeleteTask(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task == null)
        {
            return null!;
        }
        else
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return task;
        }
    }
}
