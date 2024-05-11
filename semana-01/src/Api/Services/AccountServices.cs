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
        var validationResult = await _accountValidation.ValidateAsync(loginRequest);

        if (!validationResult.IsValid)
            return ApiResult<Account>.ErrorValidation(validationResult.Errors, "Error de validacion", 400);

        var mapperAccount = _mapper.Map<Account>(loginRequest);

        var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

        if (loggetInAccount is null)
            return ApiResult<Account>.Error("Credenciales incorrectas", StatusCodes.Status401Unauthorized);

        return ApiResult<Account>.Success(loggetInAccount, "Credenciales correctas", StatusCodes.Status200OK);
    }

    public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
    {
        var response = new ApiResult<UserData>();

        var userLoginResult = await LoginUser(loginRequest);

        if (userLoginResult.ResponseMetadata.Equals(StatusCodes.Status401Unauthorized))
        {
            response.SetStatusCode(400);
            response.SetMessage("Error de validacion");
            response.Errors = userLoginResult.Errors;
            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

        UserData data = new() { User = userLoginResult.Data, Lists = _mapper.Map<IEnumerable<TaskReponseDto>>(userTasks) };

        response.SetStatusCode(StatusCodes.Status200OK);
        response.SetMessage("Tareas encontradas");
        response.SetData(data);

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
