namespace FinanceService.Domain;

public class AddUser
{
    public Guid PersonGuid { get; set; }

    public required string Login { get; set; }

    public required string Password { get; set; }
}