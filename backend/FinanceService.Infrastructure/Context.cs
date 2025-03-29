using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public required DbSet<ContributionEntity> Contributions { get; set; }
    
    public required DbSet<ExpenseCategoryEntity> ExpenseCategories { get; set; }
    
    public required DbSet<ExpenseEntity> Expenses { get; set; }
    
    public required DbSet<PersonEntity> Persons { get; set; }
    
    public required DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContributionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}