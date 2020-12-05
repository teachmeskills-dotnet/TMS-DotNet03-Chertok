using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    /// <summary>
    /// Models for request in new passord
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
