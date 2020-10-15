using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    public class RequestEmailViewModels
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
    }
}
