namespace DebtTracker.Web.Models
{
    /// <summary>
    /// Model from viewModel
    /// </summary>
    public class UsersScore
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstNameCreditor { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastNameCreditor { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleNameCreditor { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstNameDebitor { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastNameDebitor { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleNameDebitor { get; set; }

        /// <summary>
        /// Summ
        /// </summary>
        public decimal Summ { get; set; }
    }
}