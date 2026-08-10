namespace Assignment5ExpenseTrackerEnhanced.Models.DTOs
{
    /// <summary>
    /// Represents financial report metrics.
    /// </summary>
    internal class ReportDto
    {
        /// <summary>
        /// Gets the total income.
        /// </summary>
        /// <value>Represents the total income.</value>
        public decimal TotalIncome { get; init; }

        /// <summary>
        /// Gets the total expense.
        /// </summary>
        /// <value>Represents the total expense.</value>
        public decimal TotalExpense { get; init; }

        /// <summary>
        /// Gets the net balance.
        /// </summary>
        /// <value>Represents the net balance</value>
        public decimal NetBalance { get; init; }

        /// <summary>
        /// Gets the total transaction count.
        /// </summary>
        /// <value>Represents the total transactions count.</value>
        public int TransactionCount { get; init; }
    }
}
