using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    /// <summary>
    /// Model email
    /// </summary>
    public class RequestEmailViewModels
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
    }
}