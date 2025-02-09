namespace BUKEP.CommunityFund.Domain;

public class PersonEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }
    
    public string? Patronymic { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }

    public bool IsExcluded { get; set; }

    public string? ExcludeReason { get; set; }
}