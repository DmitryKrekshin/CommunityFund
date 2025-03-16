using System.ComponentModel.DataAnnotations;

namespace BUKEP.CommunityFund.WebApi;

public class UserModel
{
    [Required] public Guid PersonGuid { get; set; }

    [Required] public required string Login { get; set; }

    [Required] public required string Password { get; set; }
}