namespace AuthService.Domain;

public class UserEntity
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public Guid? PersonGuid { get; set; }

    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    public bool IsActive { get; set; }
}