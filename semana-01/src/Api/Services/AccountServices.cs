using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Data;
using Api.Models.BaseResponses;
using Api.Models.Domain;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Api.Models.Dto.Account;
using Api.Utilities.Static;
using Api.Validation;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace Api.services;

public class AccountService : IAccountService
{
	private readonly Semana01Context _db;
	private readonly IAccountRepository _accountRepository;
	private readonly ITaskRepository _taskRepository;
	private readonly IMapper _mapper;
	private readonly AccountValidation _accountValidation;
	private readonly IConfiguration _configuration;

	public AccountService(Semana01Context db, IAccountRepository accountRepository, ITaskRepository taskRepository, IMapper mapper, AccountValidation accountValidation, IConfiguration configuration)
	{
		_db = db;
		_accountRepository = accountRepository;
		_taskRepository = taskRepository;
		_mapper = mapper;
		_accountValidation = accountValidation;
		_configuration = configuration;
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

	public async Task<ApiResult<string>> LoginUser(AccountLoginRequestDto loginRequest)
	{
		var validationResult = await _accountValidation.ValidateAsync(loginRequest);

		if (!validationResult.IsValid)
			return ApiResult<string>.Error(validationResult.Errors, ReplyMessage.MESSAGE_VALIDATE, 400);

		var mapperAccount = _mapper.Map<Account>(loginRequest);

		var loggetInAccount = await _accountRepository.GetByEmailAndPasswordAsync(mapperAccount);

		if (loggetInAccount == null)
		{
			return ApiResult<string>.Error(ReplyMessage.MESSAGE_TOKEN_ERROR, StatusCodes.Status401Unauthorized);
		}

		return ApiResult<string>.Success(GenerateToken(loggetInAccount), ReplyMessage.MESSAGE_TOKEN, StatusCodes.Status200OK);
	}

	private string GenerateToken(Account account)
	{
		var jwt = _configuration.GetSection("JWT").Get<Jwt>();

		// Creating the header

		var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt!.Key));
		var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
		var header = new JwtHeader(credentials);

		// Creating Claims

		Claim[] claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
			new Claim("userId", account.UserId.ToString()),
			new Claim("userName", account.UserName),
			new Claim("email", account.Email),
		};

		// Creating the playload

		var playload = new JwtPayload(issuer: jwt.Issuer, audience: jwt.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddHours(int.Parse(jwt.Expiration)));

		// Creating the token
		var token = new JwtSecurityToken(header, playload);
		var tokenGenerate = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenGenerate;
	}

	// public async Task<ApiResult<UserData>> GetTasksForAccount(AccountLoginRequestDto loginRequest)
	// {
	// 	var userLoginResult = await LoginUser(loginRequest);

	// 	// Temporal solution
	// 	if (userLoginResult.ResponseMetadata.StatusCode == 400)
	// 	{
	// 		return ApiResult<UserData>.Error(userLoginResult.Errors, ReplyMessage.MESSAGE_VALIDATE, 400);
	// 	}
	// 	else if (userLoginResult.ResponseMetadata.StatusCode == 401)
	// 	{
	// 		return ApiResult<UserData>.Error(userLoginResult.Errors, ReplyMessage.MESSAGE_TOKEN_ERROR, 401);
	// 	}

	// 	var userTasks = await _taskRepository.GetByUserIdAsync(userLoginResult.Data!.UserId);

	// 	UserData data = new() { User = userLoginResult.Data, Lists = _mapper.Map<IEnumerable<TaskReponseDto>>(userTasks) };

	// 	return ApiResult<UserData>.Success(data, ReplyMessage.MESSAGE_QUERY, StatusCodes.Status200OK);
	// }
}
