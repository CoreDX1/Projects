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
		var mapperAccount = _mapper.Map<IEnumerable<AccountResponseDto>>(account);

		return ApiResult<IEnumerable<AccountResponseDto>>.Success(mapperAccount, "Cuentas encontradas", StatusCodes.Status200OK);
	}

	public async Task<ApiResult<Account>> PostRegister(AccountResponseDto loginRequest)
	{
		var account = _mapper.Map<Account>(loginRequest);

		var addAccount = await _accountRepository.AddAsync(account);

		if (!addAccount)
		{
			return ApiResult<Account>.Error("Cuenta ya existe", StatusCodes.Status400BadRequest);
		}

		return ApiResult<Account>.Success(account, "Cuenta registrada", StatusCodes.Status200OK);
	}

	public async Task<ApiResult<Account>> LoginUser(AccountLoginRequestDto loginRequest)
	{
		var validationResult = await _accountValidation.ValidateAsync(loginRequest);

		if (!validationResult.IsValid)
			return ApiResult<Account>.Error(validationResult.Errors, "Error de validacion", 400);

		var mapperAccount = _mapper.Map<Account>(loginRequest);

		var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

		if (loggetInAccount is null)
		{
			return ApiResult<Account>.Error("Credenciales incorrectas", StatusCodes.Status401Unauthorized);
		}

		return ApiResult<Account>.Success(loggetInAccount, "Credenciales correctas", StatusCodes.Status200OK);
	}

	public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
	{
		var userLoginResult = await LoginUser(loginRequest);

		if (userLoginResult.ResponseMetadata.StatusCode == 401)
		{
			return ApiResult<UserData>.Error(userLoginResult.Errors, "Error de validacion", 400);
		}

		var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

		UserData data = new() { User = userLoginResult.Data, Lists = _mapper.Map<IEnumerable<TaskReponseDto>>(userTasks) };

		return ApiResult<UserData>.Success(data, "Tareas encontradas", StatusCodes.Status200OK);
	}

	public async Task<ApiResult<Tasks>> DeleteTaskOfAccount(int id)
	{
		var task = await _taskRepository.DeleteTask(id);

		return ApiResult<Tasks>.Success(task, "Tarea eliminada", StatusCodes.Status200OK);
	}
}
