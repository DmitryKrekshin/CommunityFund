using System.Linq.Expressions;
using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class ExpenseCategoryRepository(Context context) : IExpenseCategoryRepository
{
    public async Task<ExpenseCategoryEntity> AddAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default)
    {
        await context.ExpenseCategories.AddAsync(expenseCategory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return expenseCategory;
    }

    public async Task<ExpenseCategoryEntity?> UpdateAsync(ExpenseCategoryEntity expenseCategory,
        CancellationToken cancellationToken = default)
    {
        var entityToUpdate =
            await context.ExpenseCategories.FirstOrDefaultAsync(f => f.Guid == expenseCategory.Guid,
                cancellationToken: cancellationToken);

        if (entityToUpdate != null)
        {
            entityToUpdate = expenseCategory;

            await context.SaveChangesAsync(cancellationToken);
        }

        return entityToUpdate;
    }

    public async Task<ExpenseCategoryEntity?> DeleteAsync(Guid expenseCategoryGuid,
        CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.ExpenseCategories.FirstOrDefaultAsync(f => f.Guid == expenseCategoryGuid,
            cancellationToken: cancellationToken);

        if (entityToDelete != null)
        {
            context.ExpenseCategories.Remove(entityToDelete);
        }

        return entityToDelete;
    }

    public async Task<IEnumerable<ExpenseCategoryEntity>> GetAsync(
        Expression<Func<ExpenseCategoryEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await context.ExpenseCategories.Where(predicate).ToListAsync(cancellationToken);
    }
}