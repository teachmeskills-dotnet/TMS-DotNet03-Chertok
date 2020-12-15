namespace DebtTracker.Web.Models
{
    public class UserProfileModel
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

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
        /// Check status
        /// </summary>
        public bool UserStatus { get; set; }
    }
}