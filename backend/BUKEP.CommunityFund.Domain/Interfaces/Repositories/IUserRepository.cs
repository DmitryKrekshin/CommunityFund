using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IUserRepository
{
    Task<UserEntity> AddAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserEntity>> GetAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}