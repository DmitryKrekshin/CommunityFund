namespace FinanceService.Domain;

public class AddExpense
{
   public Guid SpenderGuid { get; set; }

    public double Amount { get; set; }

    public required string Comment { get; set; }

    public DateTime Date { get; set; }
}