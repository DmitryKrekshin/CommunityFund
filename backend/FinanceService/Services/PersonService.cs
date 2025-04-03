using System.Data;
using System.Linq.Expressions;
using FinanceService.Domain;

namespace FinanceService;

public class PersonService(IPersonRepository personRepository) : IPersonService
{
    public async Task<PersonEntity> AddAsync(AddPerson person, CancellationToken cancellationToken = default)
    {
        var personExists = (await personRepository.GetAsync(
                p => p.Surname == person.Surname && p.Name == person.Name && p.Patronymic == person.Patronymic &&
                     p.Email == person.Email && p.PhoneNumber == person.PhoneNumber, cancellationToken))
            .ToList().Count > 0;

        if (personExists)
        {
            throw new DuplicateNameException($"Person already exists");
        }

        var personEntity = new PersonEntity
        {
            Guid = Guid.NewGuid(),
            Surname = person.Surname,
            Name = person.Name,
            Patronymic = person.Patronymic,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
            IsExcluded = false
        };

        return await personRepository.AddAsync(personEntity, cancellationToken);
    }

    public async Task<PersonEntity?> UpdateAsync(UpdatePerson person, CancellationToken cancellationToken = default)
    {
        var personEntity = (await personRepository.GetAsync(e => e.Guid == person.Guid, cancellationToken))
            .FirstOrDefault();

        if (personEntity is null)
        {
            return null;
        }

        personEntity.Surname = person.Surname;
        personEntity.Name = person.Name;
        personEntity.Patronymic = person.Patronymic;
        personEntity.Email = person.Email;
        personEntity.PhoneNumber = person.PhoneNumber;

        return await personRepository.UpdateAsync(personEntity, cancellationToken);
    }

    public async Task ExpelAsync(Guid personGuid, CancellationToken cancellationToken = default)
    {
        await ChangeIsExcludedAsync(personGuid, true, cancellationToken);
    }

    public async Task ReadmitAsync(Guid personGuid, CancellationToken cancellationToken = default)
    {
        await ChangeIsExcludedAsync(personGuid, false, cancellationToken);
    }

    private async Task ChangeIsExcludedAsync(Guid personGuid, bool isExcluded,
        CancellationToken cancellationToken = default)
    {
        var personEntity = (await personRepository.GetAsync(e => e.Guid == personGuid, cancellationToken))
            .FirstOrDefault();

        if (personEntity is null)
        {
            return;
        }

        personEntity.IsExcluded = isExcluded;

        await personRepository.UpdateAsync(personEntity, cancellationToken);
    }

    public async Task<IEnumerable<PersonEntity>> GetAsync(Expression<Func<PersonEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await personRepository.GetAsync(predicate, cancellationToken);
    }
}