using System.ComponentModel.DataAnnotations;

namespace FinanceService.WebApi;

public class PersonModel
{
    [Required] public required string Name { get; set; }

    [Required] public required string Surname { get; set; }

    public string? Patronymic { get; set; }

    [Required, Phone(ErrorMessage = "Invalid phone number")]
    public required string PhoneNumber { get; set; }

    [Required, EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }
}