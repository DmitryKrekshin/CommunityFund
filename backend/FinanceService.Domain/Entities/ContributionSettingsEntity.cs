namespace FinanceService.Domain;

public class ContributionSettingsEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid? PersonGuid { get; set; }

    public double Amount { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }
}