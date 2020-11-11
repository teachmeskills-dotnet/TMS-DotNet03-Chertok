namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// TransactionProfiles transport model
    /// </summary>
    public class TransactionProfilesDto
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Profile identifier.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Groups identifier.
        /// </summary>
        public int TransactionId { get; set; }

    }
}
