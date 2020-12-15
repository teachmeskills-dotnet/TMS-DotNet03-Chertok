using DebtTracker.Common.Constants;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DebtTracker.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Profile entity.
    /// </summary>
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Profiles)
                .HasKey(profile => profile.Id);

            builder.Property(profile => profile.UserId)
                .IsRequired();

            builder.Property(profile => profile.FirstName)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(profile => profile.LastName)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(profile => profile.MiddleName)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.HasOne(profile => profile.User)
                .WithOne(user => user.Profile)
                .HasForeignKey<Profile>(profile => profile.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}