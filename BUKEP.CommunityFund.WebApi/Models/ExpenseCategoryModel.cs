using System.ComponentModel.DataAnnotations;

namespace BUKEP.CommunityFund.WebApi;

public class ExpenseCategoryModel
{
    [Required]
    public required string Name { get; set; }
}