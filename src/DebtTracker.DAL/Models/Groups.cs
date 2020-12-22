using DebtTracker.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DebtTracker.DAL.Models
{
    /// <summary>
    /// Groups.
    /// </summary>
    public class Groups : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Title Group
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc/>
        public int ProfileId { get; set; }

        /// <summary>
        /// Navigation to Profile
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Navigation to GroupProfile.
        /// </summary>
        public ICollection<GroupProfiles> GroupsProfiles { get; set; }

        /// <summary>
        /// Navigation to Transactions.
        /// </summary>
        public ICollection<Transactions> Transactions { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// CurrencyType
        /// </summary>
        public int CurrencyType { get; set; }
    }
}