using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IExpenseRepository
{
    Task<ExpenseEntity> AddExpenseAsync(ExpenseEntity expense, CancellationToken cancellationToken = default);

    Task<ExpenseEntity> UpdateExpenseAsync(ExpenseEntity expense, CancellationToken cancellationToken = default);

    Task<ExpenseEntity> DeleteExpenseAsync(Guid expenseGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseEntity>> GetExpensesAsync(Expression<Func<ExpenseEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}