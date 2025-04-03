using System.Linq.Expressions;
using FinanceService.Domain;

namespace FinanceService;

public class ExpenseService(IExpenseRepository expenseRepository) : IExpenseService
{
    public async Task<ExpenseEntity> AddAsync(AddExpense expense, CancellationToken cancellationToken = default)
    {
        var expenseEntity = new ExpenseEntity
        {
            Guid = Guid.NewGuid(),
            SpenderGuid = expense.SpenderGuid,
            Amount = expense.Amount,
            Date = expense.Date,
            Comment = expense.Comment
        };

        return await expenseRepository.AddAsync(expenseEntity, cancellationToken);
    }

    public Task<ExpenseEntity> UpdateAsync(ExpenseEntity expense, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExpenseEntity> DeleteAsync(Guid expenseGuid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExpenseEntity>> GetAsync(Expression<Func<ExpenseEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}