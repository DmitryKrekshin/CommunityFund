namespace FinanceService.Domain;

public class ContributionEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid PayerGuid { get; set; }

    public PersonEntity? Payer { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }
}