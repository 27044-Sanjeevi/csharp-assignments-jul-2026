namespace Assignment4ExpenseTracker.Models.Enums
{
    /// <summary>
    /// Specifies the category of the source of income in a Transaction.
    /// </summary>
    internal enum SourceCategory
    {
        // Income Sources

        /// <summary>
        /// Sepcifies the income through salary.
        /// </summary>
        Salary = 1,

        /// <summary>
        /// Specifies the income through Investments.
        /// </summary>
        Investment = 2,

        /// <summary>
        /// Specifies any Miscellaneous expense.
        /// </summary>
        Miscellaneous = 3,
    }
}
