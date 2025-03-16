using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IContributionRepository
{
    Task<ContributionEntity> AddAsync(ContributionEntity contribution, CancellationToken cancellationToken = default);

    Task<ContributionEntity> UpdateAsync(ContributionEntity contribution, CancellationToken cancellationToken = default);

    Task<ContributionEntity> DeleteAsync(Guid contributionGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<ContributionEntity>> GetAsync(Expression<Func<ContributionEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}