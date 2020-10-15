namespace DebtTracker.Web.ViewModels
{
    /// <summary>
    /// Model for change password
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Old password
        /// </summary>
        public string OldPassword { get; set; }
    }
}
