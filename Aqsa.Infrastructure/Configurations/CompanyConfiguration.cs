using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure.Configurations;
internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.Phone)
           .IsRequired(false)
           .HasMaxLength(22);

        builder.HasMany(_ => _.Branches)
            .WithOne(_ => _.Company)
            .HasPrincipalKey(_ => _.Id)
            .HasForeignKey(_ => _.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(_ => _.CompanyUsers)
           .WithOne(_ => _.Company)
           .HasPrincipalKey(_ => _.Id)
           .HasForeignKey(_ => _.CompanyId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}
