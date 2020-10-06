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
    /// EF Configuration for TransactionProfiles entity.
    /// </summary>
    public class TransactionProfilesConfiguration : IEntityTypeConfiguration<TransactionProfiles>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TransactionProfiles> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.TransactionUsers)
                .HasKey(transactionprofiles => transactionprofiles.Id);

            builder.HasOne(TransactionProfiles => TransactionProfiles.Transactions)
                .WithMany(Transactions => Transactions.TransactionsProfiles)
                .HasForeignKey(TransactionProfiles => TransactionProfiles.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(TransactionProfiles => TransactionProfiles.Profile)
                .WithMany(profile => profile.TransactionsProfiles)
                .HasForeignKey(TransactionProfiles => TransactionProfiles.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
