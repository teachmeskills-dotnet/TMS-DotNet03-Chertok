using DebtTracker.Common.Constants;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebtTracker.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for GroupProfiles entity.
    /// </summary>
    public class GroupProfilesConfiguration : IEntityTypeConfiguration<GroupProfiles>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<GroupProfiles> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.GroupProfiles)
                .HasKey(GroupProfiles => GroupProfiles.Id);

            builder.HasOne(GroupProfiles => GroupProfiles.Groups)
                .WithMany(group => group.GroupsProfiles)
                .HasForeignKey(GroupProfiles => GroupProfiles.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(GroupProfiles => GroupProfiles.Profile)
                .WithMany(profile => profile.GroupsProfiles)
                .HasForeignKey(GroupProfiles => GroupProfiles.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
