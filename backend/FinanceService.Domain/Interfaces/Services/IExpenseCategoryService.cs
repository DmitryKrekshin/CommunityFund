using System.Linq.Expressions;

namespace FinanceService.Domain;

public interface IExpenseCategoryService
{
    Task<ExpenseCategoryEntity> AddAsync(string name, CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity?> UpdateAsync(Guid guid, string name, CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity?> DeleteAsync(Guid expenseCategoryGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseCategoryEntity>> GetAsync(Expression<Func<ExpenseCategoryEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}