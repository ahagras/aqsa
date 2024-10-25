using System.Reflection;
using Aqsa.Domain;
using Aqsa.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aqsa.Infrastructure
{
    public class JADbContext(DbContextOptions<JADbContext> options) : DbContext(options),IJADbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<CompanyBranch> CompanyBranches { get; set; }
        public DbSet<CompanyBranchUser> CompanyBranchUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
