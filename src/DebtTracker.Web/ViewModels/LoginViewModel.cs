using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    /// <summary>
    /// Login model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Login
        /// </summary>
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Remember question
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        /// <summary>
        ///Return Url
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
