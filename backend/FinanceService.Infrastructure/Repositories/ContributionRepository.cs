using System.Linq.Expressions;
using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class ContributionRepository(Context context) : IContributionRepository
{
    public async Task<ContributionEntity> AddAsync(ContributionEntity contribution,
        CancellationToken cancellationToken = default)
    {
        await context.Contributions.AddAsync(contribution, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return contribution;
    }

    public async Task<ContributionEntity> UpdateAsync(ContributionEntity contribution,
        CancellationToken cancellationToken = default)
    {
        var entityToUpdate =
            await context.Contributions.FirstOrDefaultAsync(f => f.Guid == contribution.Guid,
                cancellationToken: cancellationToken);

        if (entityToUpdate != null)
        {
            entityToUpdate = contribution;

            await context.SaveChangesAsync(cancellationToken);
        }

        return contribution;
    }

    public async Task<ContributionEntity?> DeleteAsync(Guid contributionGuid,
        CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Contributions.FirstOrDefaultAsync(f => f.Guid == contributionGuid,
            cancellationToken: cancellationToken);

        if (entityToDelete != null)
        {
            context.Contributions.Remove(entityToDelete);
        }

        return entityToDelete;
    }

    public async Task<IEnumerable<ContributionEntity>> GetAsync(Expression<Func<ContributionEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await context.Contributions.Where(predicate).ToListAsync(cancellationToken);
    }
}