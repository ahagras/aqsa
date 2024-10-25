using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
       builder.HasKey(_ => _.Id);

        builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.Email)
           .IsRequired()
           .HasMaxLength(100);

        builder.Property(_ => _.Password)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(_ => _.Phone)
           .IsRequired(false)
           .HasMaxLength(22);

    }
}
