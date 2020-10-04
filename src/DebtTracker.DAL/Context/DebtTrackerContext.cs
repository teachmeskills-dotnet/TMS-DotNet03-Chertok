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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
