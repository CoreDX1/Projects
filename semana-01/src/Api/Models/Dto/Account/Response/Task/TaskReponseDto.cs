namespace Api.Models.Dto.Account.Response.Task;

public class TaskReponseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}
