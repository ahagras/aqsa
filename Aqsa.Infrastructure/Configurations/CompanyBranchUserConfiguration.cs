using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure.Configurations;
internal class CompanyBranchUserConfiguration : IEntityTypeConfiguration<CompanyBranchUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompanyBranchUser> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

        builder.HasOne(_ => _.Branch)
                .WithMany(_ => _.CompanyBranchUsers)
                .HasPrincipalKey(_ => _.Id)
                .HasForeignKey(_ => _.BranchId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(_ => _.User)
                .WithMany(_ => _.CompanyBranchUsers)
                .HasPrincipalKey(_ => _.Id)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(_ => _.Company)
                .WithMany(_ => _.CompanyBranchUsers)
                .HasPrincipalKey(_ => _.Id)
                .HasForeignKey(_ => _.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
