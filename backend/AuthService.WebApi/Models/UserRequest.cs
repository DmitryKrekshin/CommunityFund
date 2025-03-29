using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApi;

public class UserRequest
{
    [Required] public required string Login { get; set; }

    [Required] public required string Password { get; set; }
}