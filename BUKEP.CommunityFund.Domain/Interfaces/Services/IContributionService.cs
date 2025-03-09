using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IContributionService
{
    Task<ContributionEntity> AddContributionAsync(ContributionEntity contribution,
        CancellationToken cancellationToken = default);

    Task<ContributionEntity> UpdateContributionAsync(ContributionEntity contribution,
        CancellationToken cancellationToken = default);

    Task<ContributionEntity> DeleteContributionAsync(Guid contributionGuid,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ContributionEntity>> GetContributionsAsync(Expression<Func<ContributionEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}