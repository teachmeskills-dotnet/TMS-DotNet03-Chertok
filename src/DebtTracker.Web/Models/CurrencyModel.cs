namespace DebtTracker.Web.Models
{
    /// <summary>
    /// Currency model.
    /// </summary>
    public class CurrencyModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Currency type (string).
        /// </summary>
        public string Type { get; set; }
    }
}