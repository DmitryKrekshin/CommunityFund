using System.Linq.Expressions;

namespace FinanceService.Domain;

public interface IPersonService
{
    Task<PersonEntity> AddAsync(PersonEntity person, CancellationToken cancellationToken = default);

    Task<PersonEntity> UpdateAsync(PersonEntity person, CancellationToken cancellationToken = default);

    Task ExpelAsync(Guid personGuid, CancellationToken cancellationToken = default);

    Task ReadmitAsync(Guid personGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<PersonEntity>> GetAsync(Expression<Func<PersonEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}