namespace AuthService.Domain;

public interface IAuthenticationService
{
    Task<UserEntity?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
}