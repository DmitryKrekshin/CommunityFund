using System.Linq.Expressions;

namespace FinanceService.Domain;

public interface IContributionSettingsRepository
{
    Task<List<ContributionSettingsEntity>> GetAsync(Expression<Func<ContributionSettingsEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<ContributionSettingsEntity> AddAsync(ContributionSettingsEntity contributionSettings,
        CancellationToken cancellationToken = default);

    Task<ContributionSettingsEntity> UpdateAsync(ContributionSettingsEntity settings,
        CancellationToken cancellationToken = default);
}