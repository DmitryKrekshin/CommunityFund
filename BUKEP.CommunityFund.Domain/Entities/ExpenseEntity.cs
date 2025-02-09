namespace BUKEP.CommunityFund.Domain;

public class ExpenseEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid SpenderGuid { get; set; }

    public required PersonEntity Spender { get; set; }

    public double Amount { get; set; }

    public required string Comment { get; set; }

    public DateTime Date { get; set; }

    public List<ExpenseCategoryEntity> ExpenseCategoryEntities { get; set; } = [];
}