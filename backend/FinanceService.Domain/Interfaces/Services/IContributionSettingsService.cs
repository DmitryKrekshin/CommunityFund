using System.Linq.Expressions;

namespace FinanceService.Domain;

public interface IContributionSettingsService
{
    Task<List<ContributionSettingsEntity>> GetAsync(Expression<Func<ContributionSettingsEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<ContributionSettingsEntity> AddAsync(ContributionSettingsEntity contributionSettingsEntity,
        CancellationToken cancellationToken = default);

    Task<ContributionSettingsEntity?> UpdateAsync(ContributionSettingsEntity contributionSettingsEntity,
        CancellationToken cancellationToken = default);
}