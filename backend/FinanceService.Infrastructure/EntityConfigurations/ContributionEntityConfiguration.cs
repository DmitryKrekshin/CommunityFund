using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

internal class ContributionEntityConfiguration : IEntityTypeConfiguration<ContributionEntity>
{
    public void Configure(EntityTypeBuilder<ContributionEntity> builder)
    {
        builder.ToTable("Contributions");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.PayerGuid).HasColumnName("PayerGuid").IsRequired();
        builder.Property(i => i.Amount).HasColumnName("Amount").IsRequired();
        builder.Property(i => i.Date).HasColumnName("Date").IsRequired();
    }
}