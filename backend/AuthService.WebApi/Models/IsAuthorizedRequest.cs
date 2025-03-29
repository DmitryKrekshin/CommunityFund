using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApi;

public class IsAuthorizedRequest
{
    [Required] public required string Token { get; set; }
}