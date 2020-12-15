using DebtTracker.Common.Constants;
using DebtTracker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DebtTracker.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Transactions entity.
    /// </summary>
    public class TransactionsConfiguration : IEntityTypeConfiguration<Transactions>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Transactions)
                .HasKey(transaction => transaction.Id);

            builder.Property(transaction => transaction.Description)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(transaction => transaction.Comment)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(transaction => transaction.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(Transactions => Transactions.Groups)
            .WithMany(group => group.Transactions)
            .HasForeignKey(Transactions => Transactions.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(Transactions => Transactions.Profile)
            .WithMany(profile => profile.Transactions)
            .HasForeignKey(Transactions => Transactions.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(transaction => transaction.Guid)
                .IsRequired();
        }
    }
}