namespace FinanceService.Domain;

public class ExpenseCategoryEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public required string Name { get; set; }
}