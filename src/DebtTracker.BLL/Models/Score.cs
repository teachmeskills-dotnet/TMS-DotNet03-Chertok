namespace DebtTracker.BLL.Models
{
    /// <summary>
    /// Score data transfer object.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Creditor
        /// </summary>
        public int Creditor { get; set; }

        /// <summary>
        /// Debitor
        /// </summary>
        public int Debitor { get; set; }

        /// <summary>
        /// Summ pay
        /// </summary>
        public decimal Summ { get; set; }
    }
}