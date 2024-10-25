using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Domain
{
    public interface IJADbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<CompanyUser> CompanyUsers { get; set; }
        DbSet<CompanyBranch> CompanyBranches { get; set; }
        DbSet<CompanyBranchUser> CompanyBranchUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
