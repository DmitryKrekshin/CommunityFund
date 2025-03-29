using FinanceService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceService.Infrastructure;

internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Guid).HasColumnName("Guid").IsRequired();
        builder.Property(i => i.PersonGuid).HasColumnName("PersonGuid").IsRequired();
        builder.Property(i => i.Login).HasColumnName("Login").IsRequired();
        builder.Property(i => i.PasswordHash).HasColumnName("PasswordHash");
        builder.Property(i => i.IsActive).HasColumnName("IsActive").IsRequired();
    }
}