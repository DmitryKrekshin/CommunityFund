using System.Linq.Expressions;
using FinanceService.Domain;

namespace FinanceService;

public class ContributionService(IContributionRepository contributionRepository) : IContributionService
{
    public async Task<ContributionEntity> AddAsync(AddContribution contribution,
        CancellationToken cancellationToken = default)
    {
        var contributionEntity = new ContributionEntity
        {
            Guid = Guid.NewGuid(),
            PayerGuid = contribution.PayerGuid,
            Amount = contribution.Amount,
            Date = contribution.Date,
            PaymentDate = DateTime.Now.ToUniversalTime()
        };

        return await contributionRepository.AddAsync(contributionEntity, cancellationToken);
    }

    public Task<ContributionEntity> UpdateAsync(ContributionEntity contribution,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ContributionEntity> DeleteAsync(Guid contributionGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ContributionEntity>> GetAsync(Expression<Func<ContributionEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await contributionRepository.GetAsync(predicate, cancellationToken);
    }
}