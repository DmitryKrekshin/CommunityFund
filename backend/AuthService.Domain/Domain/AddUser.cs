namespace AuthService.Domain;

public class AddUser
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}