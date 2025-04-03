using System.Linq.Expressions;
using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class PersonRepository(Context context) : IPersonRepository
{
    public async Task<PersonEntity> AddAsync(PersonEntity person, CancellationToken cancellationToken = default)
    {
        await context.Persons.AddAsync(person, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return person;
    }

    public async Task<PersonEntity?> UpdateAsync(PersonEntity? person, CancellationToken cancellationToken = default)
    {
        var entityToUpdate =
            await context.Persons.FirstOrDefaultAsync(f => f.Guid == person.Guid,
                cancellationToken: cancellationToken);

        if (entityToUpdate != null)
        {
            entityToUpdate = person;

            await context.SaveChangesAsync(cancellationToken);
        }

        return person;
    }

    public async Task<IEnumerable<PersonEntity>> GetAsync(Expression<Func<PersonEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await context.Persons.Where(predicate).ToListAsync(cancellationToken);
    }
}