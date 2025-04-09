using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

public class ContributionSettingsEntityConfiguration : IEntityTypeConfiguration<ContributionSettingsEntity>
{
    public void Configure(EntityTypeBuilder<ContributionSettingsEntity> builder)
    {
        builder.ToTable("ContributionSettings");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.PersonGuid).HasColumnName("PersonGuid");
        builder.Property(i => i.Amount).HasColumnName("Amount").IsRequired();
        builder.Property(i => i.DateFrom).HasColumnName("DateFrom").IsRequired();
        builder.Property(i => i.DateTo).HasColumnName("DateTo");
    }
}