namespace BUKEP.CommunityFund.WebApi;

public class ContributionModel
{
    public Guid Guid { get; set; }

    public Guid PayerGuid { get; set; }
    
    public double Amount { get; set; }

    public DateTime Date { get; set; }
}