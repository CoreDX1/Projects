using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
using Api.Utilities.Static;
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

		return ApiResult<IEnumerable<AccountResponseDto>>.Success(mapperAccount, ReplyMessage.MESSAGE_QUERY, 200);
	}

	public async Task<ApiResult<Account>> PostRegister(AccountResponseDto loginRequest)
	{
		var account = _mapper.Map<Account>(loginRequest);
		var addAccount = await _accountRepository.AddAsync(account);
		if (!addAccount)
		{
			return ApiResult<Account>.Error(ReplyMessage.MESSAGE_EXISTS, StatusCodes.Status400BadRequest);
		}
		return ApiResult<Account>.Success(account, ReplyMessage.MESSAGE_SAVE, StatusCodes.Status200OK);
	}

	public async Task<ApiResult<Account>> LoginUser(AccountLoginRequestDto loginRequest)
	{
		var validationResult = await _accountValidation.ValidateAsync(loginRequest);

		if (!validationResult.IsValid)
			return ApiResult<Account>.Error(validationResult.Errors, ReplyMessage.MESSAGE_VALIDATE, 400);

		var mapperAccount = _mapper.Map<Account>(loginRequest);

		var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

		if (loggetInAccount == null)
		{
			return ApiResult<Account>.Error(ReplyMessage.MESSAGE_TOKEN_ERROR, StatusCodes.Status401Unauthorized);
		}

		return ApiResult<Account>.Success(loggetInAccount, ReplyMessage.MESSAGE_TOKEN, StatusCodes.Status200OK);
	}

	public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
	{
		var userLoginResult = await LoginUser(loginRequest);

		// Temporal solution
		if (userLoginResult.ResponseMetadata.StatusCode == 400)
		{
			return ApiResult<UserData>.Error(userLoginResult.Errors, ReplyMessage.MESSAGE_VALIDATE, 400);
		}
		else if (userLoginResult.ResponseMetadata.StatusCode == 401)
		{
			return ApiResult<UserData>.Error(userLoginResult.Errors, ReplyMessage.MESSAGE_TOKEN_ERROR, 401);
		}

		var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

		UserData data = new() { User = userLoginResult.Data, Lists = _mapper.Map<IEnumerable<TaskReponseDto>>(userTasks) };

		return ApiResult<UserData>.Success(data, ReplyMessage.MESSAGE_QUERY, StatusCodes.Status200OK);
	}

	public async Task<ApiResult<Tasks>> DeleteTaskOfAccount(int id)
	{
		var task = await _taskRepository.DeleteTask(id);

		return ApiResult<Tasks>.Success(task, ReplyMessage.MESSAGE_DELETE, StatusCodes.Status200OK);
	}
}
