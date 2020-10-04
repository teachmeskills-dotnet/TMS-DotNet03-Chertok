using DebtTracker.Common.Interfaces;

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
        /// Profile phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Profile email.
        /// </summary>
        public string Email { get; set; }
    }
}
