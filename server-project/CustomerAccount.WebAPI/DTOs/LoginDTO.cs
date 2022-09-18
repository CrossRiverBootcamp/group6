using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebAPI.DTOs;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
