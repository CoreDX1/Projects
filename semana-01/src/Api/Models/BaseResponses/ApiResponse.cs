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
