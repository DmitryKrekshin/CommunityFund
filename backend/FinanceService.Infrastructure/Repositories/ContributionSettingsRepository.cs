using System.Linq.Expressions;
using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class ContributionSettingsRepository(Context context) : IContributionSettingsRepository
{
    public async Task<List<ContributionSettingsEntity>> GetAsync(
        Expression<Func<ContributionSettingsEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await context.ContributionSettings.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ContributionSettingsEntity> AddAsync(ContributionSettingsEntity contributionSettings,
        CancellationToken cancellationToken = default)
    {
        await context.ContributionSettings.AddAsync(contributionSettings, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return contributionSettings;
    }

    public async Task<ContributionSettingsEntity> UpdateAsync(ContributionSettingsEntity settings,
        CancellationToken cancellationToken = default)
    {
        var entityToUpdate =
            await context.ContributionSettings.FirstOrDefaultAsync(f => f.Guid == settings.Guid,
                cancellationToken: cancellationToken);

        if (entityToUpdate != null)
        {
            entityToUpdate = settings;

            await context.SaveChangesAsync(cancellationToken);
        }

        return settings;
    }
}