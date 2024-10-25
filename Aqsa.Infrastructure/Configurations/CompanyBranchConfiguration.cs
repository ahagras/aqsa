
using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure.Configurations;
internal class CompanyBranchConfiguration : IEntityTypeConfiguration<CompanyBranch>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompanyBranch> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
