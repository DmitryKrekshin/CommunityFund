using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

internal class PersonEntityConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("Persons");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.Name).HasColumnName("Name").IsRequired();
        builder.Property(i => i.Surname).HasColumnName("Surname").IsRequired();
        builder.Property(i => i.Patronymic).HasColumnName("Patronymic");
        builder.Property(i => i.PhoneNumber).HasColumnName("PhoneNumber").IsRequired();
        builder.Property(i => i.Email).HasColumnName("Email").IsRequired();
        builder.Property(i => i.IsExcluded).HasColumnName("IsExcluded").IsRequired();
        builder.Property(i => i.ExcludeReason).HasColumnName("ExcludeReason");
    }
}