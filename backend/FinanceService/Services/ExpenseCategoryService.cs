using System.Data;
using System.Linq.Expressions;
using FinanceService.Domain;

namespace FinanceService;

public class ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository) : IExpenseCategoryService
{
    public async Task<ExpenseCategoryEntity> AddAsync(string name, CancellationToken cancellationToken = default)
    {
        var expenseCategoryExists = (await expenseCategoryRepository.GetAsync(e => e.Name == name, cancellationToken))
            .ToList().Count > 0;

        if (expenseCategoryExists)
        {
            throw new DuplicateNameException($"Category with name {name} already exists");
        }

        var expenseCategoryEntity = new ExpenseCategoryEntity
        {
            Guid = Guid.NewGuid(),
            Name = name
        };

        return await expenseCategoryRepository.AddAsync(expenseCategoryEntity, cancellationToken);
    }

    public async Task<ExpenseCategoryEntity?> UpdateAsync(Guid guid, string name,
        CancellationToken cancellationToken = default)
    {
        var expenseCategoryEntity = (await expenseCategoryRepository.GetAsync(e => e.Guid == guid, cancellationToken))
            .FirstOrDefault();

        if (expenseCategoryEntity is null)
        {
            return null;
        }

        expenseCategoryEntity.Name = name;

        return await expenseCategoryRepository.UpdateAsync(expenseCategoryEntity, cancellationToken);
    }

    public async Task<ExpenseCategoryEntity?> DeleteAsync(Guid expenseCategoryGuid,
        CancellationToken cancellationToken = default)
    {
        return await expenseCategoryRepository.DeleteAsync(expenseCategoryGuid, cancellationToken);
    }

    public async Task<IEnumerable<ExpenseCategoryEntity>> GetAsync(
        Expression<Func<ExpenseCategoryEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await expenseCategoryRepository.GetAsync(predicate, cancellationToken);
    }
}