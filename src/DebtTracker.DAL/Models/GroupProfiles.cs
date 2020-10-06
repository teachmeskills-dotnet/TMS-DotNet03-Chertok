using DebtTracker.Common.Interfaces;

namespace DebtTracker.DAL.Models
{
    public class GroupProfiles : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Profile identifier.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Navigation to Profile.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Groups identifier.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Navigation to Groups.
        /// </summary>
        public Groups Groups { get; set; }
    }
}
