using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

internal class ExpenseEntityConfiguration : IEntityTypeConfiguration<ExpenseEntity>
{
    public void Configure(EntityTypeBuilder<ExpenseEntity> builder)
    {
        builder.ToTable("Expenses");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.SpenderGuid).HasColumnName("SpenderGuid").IsRequired();
        builder.Property(i => i.Amount).HasColumnName("Amount").IsRequired();
        builder.Property(i => i.Comment).HasColumnName("Comment").IsRequired();
        builder.Property(i => i.Date).HasColumnName("Date").IsRequired();
    }
}