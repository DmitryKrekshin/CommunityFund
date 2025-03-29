using AuthService.Domain;

namespace AuthService;

public class AuthenticationService(IUserRepository userRepository) : IAuthenticationService
{
    public async Task<UserEntity?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var user = (await userRepository.GetAsync(user => user.Login == username, cancellationToken)).FirstOrDefault();

        if (user is { IsActive: true } && PasswordHasher.VerifyPassword(password, user.PasswordHash))
        {
            return user;
        }

        return null;
    }
}