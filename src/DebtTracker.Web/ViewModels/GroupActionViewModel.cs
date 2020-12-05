using DebtTracker.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{

    /// <summary>
    /// Action ViewModel.
    /// </summary>
    public class GroupActionViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required (ErrorMessage = "Поле наименование не может быть пустым")]
        [StringLength(ConfigurationContants.SqlMaxLengthMedium, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Title { get; set; }

        /// <summary>
        /// Description.
        /// </summary>        
        [StringLength(ConfigurationContants.SqlMaxLengthLong, ErrorMessage = "{0} length must be between {1}.")]
        public string Description { get; set; }
    }
}
