using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    /// <summary>
    /// Register model
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required(ErrorMessage = "Не указано Имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
