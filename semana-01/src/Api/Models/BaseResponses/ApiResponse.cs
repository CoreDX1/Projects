using Api.Models.Domain.Entities;

namespace Api.Models.BaseResponses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public class Meta
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public class LoginResponse
{
    public Meta Meta { get; set; } = null!;
    public Account User { get; set; } = null!;
}


public class LoginResponse<T> : LoginResponse
{
    public T? Data { get; set; }
}



public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}
