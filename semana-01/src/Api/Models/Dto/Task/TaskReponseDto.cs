namespace Api.Models.Dto.Account;

public class TaskReponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}
