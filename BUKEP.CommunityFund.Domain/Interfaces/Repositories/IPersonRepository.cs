using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IPersonRepository
{
    Task<PersonEntity> AddPersonAsync(PersonEntity person, CancellationToken cancellationToken = default);

    Task<PersonEntity> UpdatePersonAsync(PersonEntity person, CancellationToken cancellationToken = default);

    Task<IEnumerable<PersonEntity>> GetPersonsAsync(Expression<Func<PersonEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}