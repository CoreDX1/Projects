using System.Text.Json.Serialization;

namespace Api.Models.Domain.Entities;

public partial class Account
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Tasks> TodoTasks { get; set; } = new List<Tasks>();
}
