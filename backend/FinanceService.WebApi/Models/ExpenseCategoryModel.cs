using System.ComponentModel.DataAnnotations;

namespace FinanceService.WebApi
{
    public class ExpenseCategoryModel
    {
        [Required]
        public required string Name { get; set; }
    }
}