using System.ComponentModel.DataAnnotations;

namespace FinanceService.WebApi
{
    public class ChangePasswordModel
    {
        [Required]
        public required string NewPassword { get; set; }
    }
}