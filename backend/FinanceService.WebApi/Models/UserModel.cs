using System.ComponentModel.DataAnnotations;

namespace FinanceService.WebApi
{
    public class UserModel
    {
        [Required] public Guid PersonGuid { get; set; }

        [Required] public required string Login { get; set; }

        [Required] public required string Password { get; set; }
    }
}