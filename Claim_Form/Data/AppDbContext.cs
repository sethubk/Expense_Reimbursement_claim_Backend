using Claim_Form.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Data
{
    /// <summary>
    /// Application database context responsible for
    /// managing entity models and relationships.
    /// </summary>
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        /// <summary>
        /// Employees table.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Expenses table.
        /// </summary>
        public DbSet<Expense> Expenses { get; set; }

        /// <summary>
        /// Recent claims table.
        /// </summary>
        public DbSet<RecentClaim> RecentClaims { get; set; }

        /// <summary>
        /// Travel details table.
        /// </summary>
        public DbSet<TravelDetails> TravelDetails { get; set; }

        /// <summary>
        /// International expenses table.
        /// </summary>
        public DbSet<International> Internationals { get; set; }

        /// <summary>
        /// Cash / card information table.
        /// </summary>
        public DbSet<CashInfo> CashInfos { get; set; }

        public DbSet<Domestic>Domestics{ get; set; }

        #endregion

        /// <summary>
        /// Configures entity relationships and constraints using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee → RecentClaims (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.RecentClaims)
                .WithOne(rc => rc.Employee)
                .HasForeignKey(rc => rc.EmpId)
                .OnDelete(DeleteBehavior.Restrict);

            // RecentClaim → Expenses (One-to-Many)
            modelBuilder.Entity<RecentClaim>()
                .HasMany(rc => rc.Expenses)
                .WithOne(e => e.RecentClaim)
                .HasForeignKey(e => e.RecentClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            // RecentClaim → TravelDetails (One-to-One)
            modelBuilder.Entity<RecentClaim>()
                .HasOne(rc => rc.TravelDetails)
                .WithOne(td => td.RecentClaim)
                .HasForeignKey<TravelDetails>(td => td.RecentClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            // TravelDetails → CashInfo (One-to-Many)
            modelBuilder.Entity<TravelDetails>()
                .HasMany(td => td.CardCashEntries)
                .WithOne(ci => ci.TravelDetails)
                .HasForeignKey(ci => ci.TravelId);
                

            // TravelDetails → International (One-to-Many)
            modelBuilder.Entity<TravelDetails>()
                .HasMany(td => td.Internationals)
                .WithOne(i => i.TravelDetails)
                .HasForeignKey(i => i.TravelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelDetails>()
               .HasMany(td => td.Domestics)
               .WithOne(i => i.TravelDetails)
               .HasForeignKey(i => i.TravelId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}