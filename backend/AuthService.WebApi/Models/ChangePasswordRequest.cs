using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApi;

public class ChangePasswordRequest
{
    [Required]
    public required string NewPassword { get; set; }
}