namespace FinanceService.WebApi;

public class ContributionSettings
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid? PersonGuid { get; set; }

    public double Amount { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }
}