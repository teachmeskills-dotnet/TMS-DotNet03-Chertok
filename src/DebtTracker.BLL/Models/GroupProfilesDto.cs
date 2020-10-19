namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// Transport model from groupeprofile
    /// </summary>
    public class GroupProfilesDto
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
        public int GroupId { get; set; }

    }
}
