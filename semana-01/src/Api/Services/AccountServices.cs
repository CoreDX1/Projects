using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
using AutoMapper;

namespace Api.Services;

public class AccountService : IAccountService
{
    private readonly Semana01Context _db;
    private readonly IAccountRepository _accountRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public AccountService(Semana01Context db, IAccountRepository accountRepository, ITaskRepository taskRepository, IMapper mapper)
    {
        _db = db;
        _accountRepository = accountRepository;
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<AccountResponseDto>>> GetAllAsync()
    {
        var account = await _accountRepository.GetAllAsync();

        var response = new ApiResponse<IEnumerable<AccountResponseDto>>()
        {
            Data = _mapper.Map<IEnumerable<AccountResponseDto>>(account),
            IsSuccess = true,
            Message = "Cuentas encontradas",
            StatusCode = StatusCodes.Status200OK
        };

        return response;
    }

    public async Task<ApiResponse<Account>> PostRegister(AccountResponseDto account)
    {
        var response = new ApiResponse<Account>();

        Account newAccount = _mapper.Map<Account>(account);

        var addAccount = await _accountRepository.AddAsync(newAccount);

        if (!addAccount)
        {
            response.IsSuccess = false;
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = "Cuenta ya existe";
        }
        else
        {
            response.IsSuccess = true;
            response.StatusCode = StatusCodes.Status200OK;
            response.Message = "Cuenta creada";
        }

        return response;
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

    public async Task<LoginResponse> GetTasksByAccount(AccountLoginRequestDto account)
    {
        var response = new LoginResponse();

        var loggetInAccount = await LoginUser(account);

        if (!loggetInAccount.IsSuccess)
        {
            response.Meta.StatusCode = 200;
            response.Meta.Message = "Error al buscar tareas";

            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(loggetInAccount.Data!.UserId);

        response.Meta.StatusCode = 200;
        response.Meta.Message = "Tareas encontradas";
        response.Data.User = loggetInAccount.Data;
        response.Data.Lists = _mapper.Map<IEnumerable<TaskReponseDto>>(userTasks);

        return response;
    }

    public async Task<Tasks> DeleteTask(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task == null)
        {
            return new Tasks();
        }
        else
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return task;
        }
    }
}
