using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

internal class ExpenseCategoryEntityConfiguration : IEntityTypeConfiguration<ExpenseCategoryEntity>
{
    public void Configure(EntityTypeBuilder<ExpenseCategoryEntity> builder)
    {
        builder.ToTable("ExpenseCategories");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.Name).HasColumnName("Name").IsRequired();
    }
}