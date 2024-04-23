
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.Account.Request;

public class AccountDto
{
    [Required(ErrorMessage = "UserName is required")]
    [StringLength(20)]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
