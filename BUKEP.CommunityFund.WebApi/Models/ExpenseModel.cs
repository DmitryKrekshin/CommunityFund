namespace BUKEP.CommunityFund.WebApi;

public class ExpenseModel
{
    public Guid SpenderGuid { get; set; }
    
    public double Amount { get; set; }

    public required string Comment { get; set; }

    public DateTime Date { get; set; }
}