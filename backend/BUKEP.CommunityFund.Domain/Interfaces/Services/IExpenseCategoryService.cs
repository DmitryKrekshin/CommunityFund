using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IExpenseCategoryService
{
    Task<ExpenseCategoryEntity> AddAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity> UpdateAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity> DeleteAsync(Guid expenseCategoryGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseCategoryEntity>> GetAsync(Expression<Func<ExpenseCategoryEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}