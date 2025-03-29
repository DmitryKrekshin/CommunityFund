using System.Linq.Expressions;

namespace FinanceService.Domain;

public interface IExpenseService
{
    Task<ExpenseEntity> AddAsync(ExpenseEntity expense, CancellationToken cancellationToken = default);

    Task<ExpenseEntity> UpdateAsync(ExpenseEntity expense, CancellationToken cancellationToken = default);

    Task<ExpenseEntity> DeleteAsync(Guid expenseGuid, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseEntity>> GetAsync(Expression<Func<ExpenseEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}