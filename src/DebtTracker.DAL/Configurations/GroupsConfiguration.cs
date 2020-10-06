using DebtTracker.Common.Constants;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DebtTracker.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Groups entity.
    /// </summary>
    public class GroupsConfiguration : IEntityTypeConfiguration<Groups>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Groups)
                .HasKey(group => group.Id);

            builder.Property(group => group.Title)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(group => group.Description)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.HasOne(Profile => Profile.Profile)
            .WithMany(group => group.Groups)
            .HasForeignKey(group => group.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
