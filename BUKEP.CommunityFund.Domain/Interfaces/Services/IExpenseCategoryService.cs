using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IExpenseCategoryService
{
    Task<ExpenseCategoryEntity> AddExpenseCategoryAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity> UpdateExpenseCategoryAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default);

    Task<ExpenseCategoryEntity> DeleteExpenseCategoryAsync(Guid expenseCategoryGuid,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseCategoryEntity>> GetExpenseCategoriesAsync(
        Expression<Func<ExpenseCategoryEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}