namespace Assignment5ExpenseTrackerEnhanced.Models.Enums
{
    /// <summary>
    /// Specifies the flow of cash in a Transaction.
    /// </summary>
    internal enum TransactionType
    {
        /// <summary>
        /// Specifies an inflow of cash or revenue.
        /// </summary>
        Income = 1,

        /// <summary>
        /// Specifies the cash spent.
        /// </summary>
        Expense = 2,
    }
}
