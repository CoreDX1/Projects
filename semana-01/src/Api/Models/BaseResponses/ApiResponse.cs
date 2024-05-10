using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.BaseResponses;

public class ApiResult
{
    public ResponseMetadata ResponseMetadata { get; } = new();

    public Dictionary<string, List<string>> Errors { get; set; } = new();

    public void SetError(List<FluentValidation.Results.ValidationFailure> validateError)
    {
        var error = new Dictionary<string, List<string>>();

        foreach (var item in validateError)
        {
            if (!error.ContainsKey(item.PropertyName))
                error.Add(item.PropertyName, new List<string> { item.ErrorMessage });
            else
                error[item.PropertyName].Add(item.ErrorMessage);
        }

        Errors = error;
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

public class ApiResult<T> : ApiResult
{
    public T? Data { get; set; }

    public void SetData(T data)
    {
        Data = data;
    }
}

public class ValidationError
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UserData
{
    public Account User { get; set; } = new Account();
    public IEnumerable<TaskReponseDto>? Lists { get; set; } = new List<TaskReponseDto>();
}

public class ResponseMetadata
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
