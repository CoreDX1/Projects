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

        var response = new ApiResult<IEnumerable<AccountResponseDto>>();

        response.SetStatusCode(StatusCodes.Status200OK);
        response.SetMessage("Cuentas encontradas");
        response.SetData(_mapper.Map<IEnumerable<AccountResponseDto>>(account));

        return response;
    }

    public async Task<ApiResult<Account>> PostRegister(AccountResponseDto loginRequest)
    {
        var response = new ApiResult<Account>();

        var account = _mapper.Map<Account>(loginRequest);

        var addAccount = await _accountRepository.AddAsync(account);

        if (!addAccount)
        {
            response.SetStatusCode(StatusCodes.Status400BadRequest);
            response.SetMessage("Cuenta ya existe");
        }
        else
        {
            response.SetStatusCode(StatusCodes.Status200OK);
            response.SetMessage("Cuenta creada");
        }

        return response;
    }

    public async Task<ApiResult<Account>> LoginUser(AccountLoginRequestDto loginRequest)
    {
        var response = new ApiResult<Account>();

        var validationResult = await _accountValidation.ValidateAsync(loginRequest);

        if (!validationResult.IsValid)
        {
            response.SetMessage("Error de validacion");
            response.SetStatusCode(StatusCodes.Status400BadRequest);
            response.SetError(validationResult.Errors);
            return response;
        }

        var mapperAccount = _mapper.Map<Account>(loginRequest);

        var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

        if (loggetInAccount == null)
        {
            response.SetStatusCode(StatusCodes.Status401Unauthorized);
            response.SetMessage("Credenciales incorrectas");
            response.SetData(new Account());
        }
        else
        {
            response.SetStatusCode(StatusCodes.Status200OK);
            response.SetMessage("Credenciales correctas");
            response.SetData(loggetInAccount);
        }
        return response;
    }

    public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
    {
        ApiResult<UserData> response = new();

        var userLoginResult = await LoginUser(loginRequest);
        var statuscode = userLoginResult.ResponseMetadata.StatusCode;
        var message = userLoginResult.ResponseMetadata.Message;
        var user = userLoginResult.Data;

        if (!statuscode.Equals(200))
        {
            response.SetStatusCode(statuscode);
            response.SetMessage(message);
            response.Errors = userLoginResult.Errors;

            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data.UserId);

        response.SetStatusCode(StatusCodes.Status200OK);
        response.SetMessage("Tareas encontradas");

        response.Data.User = userLoginResult.Data;
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
