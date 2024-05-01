using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;

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

    public async Task<ApiResponse<IEnumerable<Account>>> GetAllAsync()
    {
        var account = await _accountRepository.GetAllAsync();

        var response = new ApiResponse<IEnumerable<Account>>()
        {
            Data = account,
            Message = "Cuentas encontradas",
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK
        };

        return response;
    }

    /* El método PostRegister crea una nueva cuenta, pero no verifica si la cuenta ya existe.  */
    /* Podría considerarse agregar una verificación para evitar la creación de cuentas duplicadas. */
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

    public async Task<ApiResponse<Account>> LoginUser(AccountLoginRequestDto account)
    {
        var response = new ApiResponse<Account>();

        var mapperAcount = new Account() { Password = account.Password, Email = account.Email };

        var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAcount);

        if (loggetInAccount == null)
        {
            response.IsSuccess = false;
            response.StatusCode = StatusCodes.Status401Unauthorized;
            response.Message = "Credenciales incorrectas";
            response.Data = null;
        }
        else
        {
            response.IsSuccess = true;
            response.StatusCode = StatusCodes.Status200OK;
            response.Message = "Credenciales correctas";
            response.Data = loggetInAccount;
        }
        return response;
    }

    public async Task<ApiResponse<IEnumerable<TaskReponseDto>>> GetTasksByAccount(AccountLoginRequestDto account)
    {
        var response = new ApiResponse<IEnumerable<TaskReponseDto>>();
        var loggetInAccount = await LoginUser(account);

        if (!loggetInAccount.IsSuccess)
        {
            response.IsSuccess = false;
            response.Message = loggetInAccount.Message;
            response.Data = null;

            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(loggetInAccount.Data!.UserId);

        var taskReponseDtoList = userTasks.Select(task => new TaskReponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Completed = task.Completed,
            Description = task.Description
        });

        response.IsSuccess = true;
        response.StatusCode = StatusCodes.Status200OK;
        response.Message = "Tareas encontradas";
        response.Data = taskReponseDtoList;

        return response;
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
