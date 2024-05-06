namespace Api.Models.Dto.Account;

public class AccountResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}
