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

    public async Task<ApiResponse<Account>> PostRegister(AccountResponseDto loginRequest)
    {
        var response = new ApiResponse<Account>();

        var account = _mapper.Map<Account>(loginRequest);

        var addAccount = await _accountRepository.AddAsync(account);

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

    public async Task<ApiResponse<Account>> LoginUser(AccountLoginRequestDto loginRequest)
    {
        var response = new ApiResponse<Account>();

        var erros = new Dictionary<string, List<string>>();

        var validationResult = await _accountValidation.ValidateAsync(loginRequest);

        foreach (var error in validationResult.Errors)
        {
            if (!erros.ContainsKey(error.PropertyName))
            {
                erros.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
            else
            {
                erros[error.PropertyName].Add(error.ErrorMessage);
            }
        }

        if (!validationResult.IsValid)
        {
            response.IsSuccess = false;
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = "Error de validacion";
            response.Errors = erros;
        }

        var mapperAccount = _mapper.Map<Account>(loginRequest);

        var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

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

    public async Task<LoginResponse> GetTasksForAccount(AccountLoginRequestDto loginRequest)
    {
        var response = new LoginResponse();

        var userLoginResult = await LoginUser(loginRequest);

        if (!userLoginResult.IsSuccess)
        {
            response.Meta.StatusCode = StatusCodes.Status401Unauthorized;
            response.Meta.Message = "Error al buscar tareas";

            return response;
        }

        var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

        response.Meta.StatusCode = 200;
        response.Meta.Message = "Tareas encontradas";
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
