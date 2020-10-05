using DebtTracker.DAL.Configurations;
using DebtTracker.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DebtTracker.DAL.Context
{
    /// <summary>
    /// Database context
    /// </summary>
    public class DebtTrackerContext : IdentityDbContext<User>
    {
        public DebtTrackerContext(DbContextOptions<DebtTrackerContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Profiles
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Groups
        /// </summary>
        public DbSet<Groups> Groups { get; set; }

        /// <summary>
        /// Transactions
        /// </summary>
        public DbSet<Transactions> Transactions { get; set; }

        /// <summary>
        /// GroupProfiles
        /// </summary>
        public DbSet<GroupProfiles> GroupProfiles { get; set; }

        /// <summary>
        /// TransactionProfiles
        /// </summary>
        public DbSet<TransactionProfiles> TransactionProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new GroupsConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionsConfiguration());
            modelBuilder.ApplyConfiguration(new GroupProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionProfilesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
