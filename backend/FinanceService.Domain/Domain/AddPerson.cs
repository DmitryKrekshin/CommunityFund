namespace FinanceService.Domain;

public class AddPerson
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? Patronymic { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }
}