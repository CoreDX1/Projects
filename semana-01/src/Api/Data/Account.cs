namespace Api.Data;

public partial class Account
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int Password { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
