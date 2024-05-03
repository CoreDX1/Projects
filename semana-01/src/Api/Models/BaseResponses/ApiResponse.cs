using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;

namespace Api.Models.BaseResponses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
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



