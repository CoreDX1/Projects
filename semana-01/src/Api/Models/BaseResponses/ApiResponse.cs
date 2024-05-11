using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.BaseResponses;

public class ApiResult<T>
{
	public ResponseMetadata ResponseMetadata { get; set; } = new ResponseMetadata();

	public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

	public T? Data { get; set; }

	public void SetData(T data)
	{
		Data = data;
	}

	public static ApiResult<T> Success(T data, string message = "Operacion exitosa", int code = 200)
	{
		return new ApiResult<T>
		{
			Data = data,
			ResponseMetadata = new ResponseMetadata { Message = message, StatusCode = code },
		};
	}

	public static ApiResult<T> Success(string message, int code)
	{
		return new ApiResult<T>
		{
			ResponseMetadata = new ResponseMetadata { Message = message, StatusCode = code },
		};
	}

	public static ApiResult<T> Error(string message, int code)
	{
		return new ApiResult<T>
		{
			ResponseMetadata = new ResponseMetadata { Message = message, StatusCode = code }
		};
	}

	public static ApiResult<T> Error(List<FluentValidation.Results.ValidationFailure> validateError, string message, int code)
	{
		return new ApiResult<T>
		{
			Errors = SetError(validateError),
			Data = default,
			ResponseMetadata = new ResponseMetadata { Message = message, StatusCode = code }
		};
	}

	public static ApiResult<T> Error(Dictionary<string, List<string>> validateError, string message, int code)
	{
		return new ApiResult<T>
		{
			Errors = validateError,
			Data = default,
			ResponseMetadata = new ResponseMetadata { Message = message, StatusCode = code }
		};
	}

	/// <summary>
	///  Crea un nuevo objeto de errores
	/// </summary>
	/// <param name="validateError">Lista de errores</param>
	public static Dictionary<string, List<string>> SetError(List<FluentValidation.Results.ValidationFailure> validateError)
	{
		var error = new Dictionary<string, List<string>>();

		foreach (var item in validateError)
		{
			if (!error.ContainsKey(item.PropertyName))
				error.Add(item.PropertyName, new List<string> { item.ErrorMessage });
			else
				error[item.PropertyName].Add(item.ErrorMessage);
		}

		return error;
	}

	public void SetStatusCode(int code)
	{
		ResponseMetadata.StatusCode = code;
	}

	public void SetMessage(string message)
	{
		ResponseMetadata.Message = message;
	}
}

public class ValidationError
{
	public string Password { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
}

public class UserData
{
	public Account User { get; set; }

	public IEnumerable<TaskReponseDto> Lists { get; set; } = new List<TaskReponseDto>();

	public UserData()
	{
		User = new Account();
	}
}

public class ResponseMetadata
{
	public int StatusCode { get; set; }
	public string Message { get; set; } = string.Empty;
}
