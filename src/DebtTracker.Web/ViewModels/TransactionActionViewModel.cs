using DebtTracker.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace DebtTracker.Web.ViewModels
{
    public class TransactionActionViewModel
    {

        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required(ErrorMessage = "Укажите наименование")]
        [StringLength(ConfigurationContants.SqlMaxLengthMedium, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Description { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [Required(ErrorMessage = "Укажите сумму транзакции")]
        [Range(typeof(decimal), "0", "9999999999999999,99", ErrorMessage = "Числовое значение, разделитель запятая")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        //public string CurrencyType { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <inheritdoc/>
        public int ProfileId { get; set; }

        /// <inheritdoc/>
        public int GroupId { get; set; }
    }
}
