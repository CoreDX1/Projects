using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.BaseResponses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}

public class ValidationError
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

/// Login
public class Data
{
    public Account User { get; set; } = new Account();
    public IEnumerable<TaskReponseDto>? Lists { get; set; } = new List<TaskReponseDto>();
}

public class Meta
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public record LoginResponse
{
    public Meta Meta { get; set; } = new Meta();
    public Data Data { get; set; } = new Data();
}
///
