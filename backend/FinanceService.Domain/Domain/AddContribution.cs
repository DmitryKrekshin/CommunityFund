namespace FinanceService.Domain;

public class AddContribution
{
    public Guid PayerGuid { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }
}