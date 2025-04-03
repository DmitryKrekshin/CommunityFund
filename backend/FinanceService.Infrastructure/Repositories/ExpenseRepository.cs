using System.Linq.Expressions;
using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class ExpenseRepository(Context context) : IExpenseRepository
{
    public async Task<ExpenseEntity> AddAsync(ExpenseEntity expense, CancellationToken cancellationToken = default)
    {
        await context.Expenses.AddAsync(expense, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return expense;
    }

    public async Task<ExpenseEntity> UpdateAsync(ExpenseEntity expense, CancellationToken cancellationToken = default)
    {
        var entityToUpdate =
            await context.Expenses.FirstOrDefaultAsync(f => f.Guid == expense.Guid,
                cancellationToken: cancellationToken);

        if (entityToUpdate != null)
        {
            entityToUpdate = expense;

            await context.SaveChangesAsync(cancellationToken);
        }

        return expense;
    }

    public async Task<ExpenseEntity?> DeleteAsync(Guid expenseGuid, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Expenses.FirstOrDefaultAsync(f => f.Guid == expenseGuid,
            cancellationToken: cancellationToken);

        if (entityToDelete != null)
        {
            context.Expenses.Remove(entityToDelete);
        }

        return entityToDelete;
    }

    public async Task<IEnumerable<ExpenseEntity>> GetAsync(Expression<Func<ExpenseEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await context.Expenses.Where(predicate).ToListAsync(cancellationToken);
    }
}