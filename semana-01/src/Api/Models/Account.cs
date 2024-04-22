namespace Api.Models;

public partial class Account
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int Password { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual ICollection<Tasks> TodoTasks { get; set; } = new List<Tasks>();
}
