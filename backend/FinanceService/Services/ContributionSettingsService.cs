using System.Linq.Expressions;
using FinanceService.Domain;

namespace FinanceService;

public class ContributionSettingsService(IContributionSettingsRepository contributionSettingsRepository)
    : IContributionSettingsService
{
    public async Task<List<ContributionSettingsEntity>> GetAsync(
        Expression<Func<ContributionSettingsEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await contributionSettingsRepository.GetAsync(predicate, cancellationToken);
    }

    public async Task<ContributionSettingsEntity> AddAsync(ContributionSettingsEntity contributionSettingsEntity,
        CancellationToken cancellationToken = default)
    {
        contributionSettingsEntity.Guid = Guid.NewGuid();
        return await contributionSettingsRepository.AddAsync(contributionSettingsEntity, cancellationToken);
    }

    public async Task<ContributionSettingsEntity?> UpdateAsync(ContributionSettingsEntity contributionSettingsEntity,
        CancellationToken cancellationToken = default)
    {
        var expenseCategoryEntity =
            (await contributionSettingsRepository.GetAsync(e => e.Guid == contributionSettingsEntity.Guid,
                cancellationToken)).FirstOrDefault();

        if (expenseCategoryEntity is null)
        {
            return null;
        }

        expenseCategoryEntity.Amount = contributionSettingsEntity.Amount;
        expenseCategoryEntity.PersonGuid = contributionSettingsEntity.PersonGuid;
        expenseCategoryEntity.DateFrom = contributionSettingsEntity.DateFrom;
        expenseCategoryEntity.DateTo = contributionSettingsEntity.DateTo;

        return await contributionSettingsRepository.UpdateAsync(expenseCategoryEntity, cancellationToken);
    }
}