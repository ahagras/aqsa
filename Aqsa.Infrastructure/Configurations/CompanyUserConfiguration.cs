using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure.Configurations;
internal class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompanyUser> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

        builder.HasOne(_ => _.User)
                .WithMany(_ => _.CompanyUsers)
                .HasPrincipalKey(_ => _.Id)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(_ => _.Company)
                .WithMany(_ => _.CompanyUsers)
                .HasPrincipalKey(_ => _.Id)
                .HasForeignKey(_ => _.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
