using System.Data;
using System.Linq.Expressions;
using AuthService.Domain;

namespace AuthService;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserEntity> AddAsync(AddUser addUser, CancellationToken cancellationToken = default)
    {
        var userExists = (await userRepository.GetAsync(user => user.Login == addUser.Login, cancellationToken))
            .ToList().Count > 0;

        if (userExists)
        {
            throw new DuplicateNameException($"User with login {addUser.Login} already exists");
        }

        var userEntity = new UserEntity
        {
            Guid = Guid.NewGuid(),
            Login = addUser.Login,
            PasswordHash = PasswordHasher.HashPassword(addUser.Password),
            IsActive = true
        };

        return await userRepository.AddAsync(userEntity, cancellationToken);
    }

    public async Task BlockAsync(Guid userGuid, CancellationToken cancellationToken = default)
    {
        await ChangeUserStatus(userGuid, false, cancellationToken);
    }

    public async Task UnblockAsync(Guid userGuid, CancellationToken cancellationToken = default)
    {
        await ChangeUserStatus(userGuid, true, cancellationToken);
    }

    private async Task ChangeUserStatus(Guid userGuid, bool isActive, CancellationToken cancellationToken = default)
    {
        var userToUnblock = (await userRepository.GetAsync(user => user.Guid == userGuid, cancellationToken))
            .FirstOrDefault();

        if (userToUnblock is null)
        {
            return;
        }

        userToUnblock.IsActive = isActive;

        await userRepository.UpdateAsync(userToUnblock, cancellationToken);
    }

    public async Task ChangePasswordAsync(Guid userGuid, string newPassword,
        CancellationToken cancellationToken = default)
    {
        var userUpdate = (await userRepository.GetAsync(user => user.Guid == userGuid, cancellationToken))
            .FirstOrDefault();

        if (userUpdate is null)
        {
            return;
        }

        userUpdate.PasswordHash = PasswordHasher.HashPassword(newPassword);

        await userRepository.UpdateAsync(userUpdate, cancellationToken);
    }

    public async Task<IEnumerable<UserEntity>> GetAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await userRepository.GetAsync(predicate, cancellationToken);
    }
}