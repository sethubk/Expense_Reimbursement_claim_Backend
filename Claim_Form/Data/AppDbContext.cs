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

        public DbSet<TravelDetails> TravelDetails { get; set; }

        public DbSet<International>Internationals { get; set; }
        public DbSet<CashInfo> CashInfo { get; set; }
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

            modelBuilder.Entity<RecentClaim>()
                .HasOne(rc => rc.TravelDetails)
                .WithOne(td => td.RecentClaim)
                .HasForeignKey<TravelDetails>(td => td.RecentClaimId);


            modelBuilder.Entity<TravelDetails>()
                    .HasMany(td => td.CardCashEntries)
                    .WithOne(ci => ci.TravelDetails)
                    .HasForeignKey(ci => ci.TravelId);


            modelBuilder.Entity<TravelDetails>()
                .HasMany(rc => rc.Internationals)
                .WithOne(ie => ie.TravelDetails)
                .HasForeignKey(ie => ie.TravelId); 

             

        }
    }
}
