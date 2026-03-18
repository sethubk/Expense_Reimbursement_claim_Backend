using Claim_Form.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<RecentClaim> RecentClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecentClaim>()
                .HasMany(e => e.Expenses)
                .WithOne(ex => ex.RecentClaim)
                .HasForeignKey(ex => ex.RecentClaimId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.RecentClaims)
                .WithOne(rc => rc.Employee)
                .HasForeignKey(rc => rc.EmpId);
        }
    }
}
