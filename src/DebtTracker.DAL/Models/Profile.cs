using DebtTracker.Common.Interfaces;
using System.Collections.Generic;

namespace DebtTracker.DAL.Models
{
    /// <summary>
    /// Profile
    /// </summary>
    public class Profile : IHasDbIdentity, IHasUserIdentity
    {

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation to User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Navigation to GroupProfile.
        /// </summary>
        public ICollection<GroupProfiles> GroupsProfiles { get; set; }

        /// <summary>
        /// Navigation to TransactionProfiles.
        /// </summary>
        public ICollection<TransactionProfiles> TransactionsProfiles { get; set; }

        /// <summary>
        /// Navigation to Transactions.
        /// </summary>
        public ICollection<Transactions> Transactions { get; set; }

        /// <summary>
        /// Navigation to Transactions.
        /// </summary>
        public ICollection<Groups> Groups { get; set; }
    }
}
