using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IUserRepository
{
    Task<UserEntity> AddUserAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<UserEntity> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserEntity>> GetUsersAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}