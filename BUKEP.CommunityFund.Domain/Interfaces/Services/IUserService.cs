using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IUserService
{
    Task<UserEntity> AddUserAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task BlockUserAsync(Guid userGuid, CancellationToken cancellationToken = default);

    Task UnblockUserAsync(Guid userGuid, CancellationToken cancellationToken = default);

    Task ChangePasswordAsync(Guid userGuid, string oldPassword, string newPassword,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<UserEntity>> GetUsersAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}