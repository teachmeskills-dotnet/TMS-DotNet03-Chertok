namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// Transport model from Profile
    /// </summary>
    public class ProfileDto
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
    }
}