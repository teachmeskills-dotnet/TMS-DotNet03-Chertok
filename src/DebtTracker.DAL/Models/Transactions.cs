using DebtTracker.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebtTracker.DAL.Models
{
    /// <summary>
    /// Transactions
    /// </summary>
    public class Transactions : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// CurrencyType
        /// </summary>
        public int CurrencyType { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <inheritdoc/>
        public int ProfileId { get; set; }

        /// <summary>
        /// Navigation to Profile
        /// </summary>
        public Profile Profile { get; set; }

        /// <inheritdoc/>
        public int GroupId { get; set; }

        /// <summary>
        /// Navigation to Group
        /// </summary>
        public Groups Groups { get; set; }

        /// <summary>
        /// Navigation to TransactionProfiles.
        /// </summary>
        public ICollection<TransactionProfiles> TransactionsProfiles { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }
    }
}
