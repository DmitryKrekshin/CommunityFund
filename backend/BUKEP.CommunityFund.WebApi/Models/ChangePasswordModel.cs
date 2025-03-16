using System.ComponentModel.DataAnnotations;

namespace BUKEP.CommunityFund.WebApi;

public class ChangePasswordModel
{
    [Required]
    public required string NewPassword { get; set; }
}