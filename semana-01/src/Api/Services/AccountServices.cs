using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
using Api.Validation;
using AutoMapper;

namespace Api.Services;

public class AccountService : IAccountService
{
    private readonly Semana01Context _db;
    private readonly IAccountRepository _accountRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly AccountValidation _accountValidation;

    public AccountService(Semana01Context db, IAccountRepository accountRepository, ITaskRepository taskRepository, IMapper mapper, AccountValidation accountValidation)
    {
        _db = db;
        _accountRepository = accountRepository;
        _taskRepository = taskRepository;
        _mapper = mapper;
        _accountValidation = accountValidation;
    }

    public async Task<ApiResult<IEnumerable<AccountResponseDto>>> GetAllAsync()
    {
        var account = await _accountRepository.GetAllAsync();

        var response = new ApiResult<IEnumerable<AccountResponseDto>>()
        {
            Data = _mapper.Map<IEnumerable<AccountResponseDto>>(account),
            ResponseMetadata = { Message = "Cuentas encontradas", StatusCode = StatusCodes.Status200OK },
        };

        return response;
    }

    public async Task<ApiResult<Account>> PostRegister(AccountResponseDto loginRequest)
    {
        var response = new ApiResult<Account>();

        var account = _mapper.Map<Account>(loginRequest);

        var addAccount = await _accountRepository.AddAsync(account);

        if (!addAccount)
        {
            response.ResponseMetadata.StatusCode = StatusCodes.Status400BadRequest;
            response.ResponseMetadata.Message = "Cuenta ya existe";
        }
        else
        {
            response.ResponseMetadata.StatusCode = StatusCodes.Status200OK;
            response.ResponseMetadata.Message = "Cuenta creada";
        }

        return response;
    }

    public async Task<ApiResult<Account>> LoginUser(AccountLoginRequestDto loginRequest)
    {
        var response = new ApiResult<Account>();

        var error = new Dictionary<string, List<string>>();

        var validationResult = await _accountValidation.ValidateAsync(loginRequest);

        if (!validationResult.IsValid)
        {
            response.ResponseMetadata.StatusCode = StatusCodes.Status400BadRequest;
            response.ResponseMetadata.Message = "Error de validacion";
            response.SetError(validationResult.Errors);
            return response;
        }

        var mapperAccount = _mapper.Map<Account>(loginRequest);

        var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

        if (loggetInAccount == null)
        {
            response.ResponseMetadata.StatusCode = StatusCodes.Status401Unauthorized;
            response.ResponseMetadata.Message = "Credenciales incorrectas";
            response.Data = new Account();
        }
        else
        {
            response.ResponseMetadata.StatusCode = StatusCodes.Status200OK;
            response.ResponseMetadata.Message = "Credenciales correctas";
            response.Data = loggetInAccount;
        }
        return response;
    }

    public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
    {
        var response = new ApiResult<UserData>();

        var userLoginResult = await LoginUser(loginRequest);

        if (!userLoginResult.ResponseMetadata.StatusCode.Equals(200))
        {
            response.ResponseMetadata.StatusCode = userLoginResult.ResponseMetadata.StatusCode;
            response.ResponseMetadata.Message = userLoginResult.ResponseMetadata.Message;
            // response.Errors = userLoginResult.Errors;

            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

        response.ResponseMetadata.StatusCode = StatusCodes.Status200OK;
        response.ResponseMetadata.Message = "Tareas encontradas";
        response.Data!.User = userLoginResult.Data;
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
