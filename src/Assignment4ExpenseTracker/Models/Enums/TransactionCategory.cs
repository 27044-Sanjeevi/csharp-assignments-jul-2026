namespace Assignment4ExpenseTracker.Models.Enums
{
    /// <summary>
    /// Specifies the category of transaction including income and expense.
    /// </summary>
    internal enum TransactionCategory
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
        /// Specifies any Miscellaneous income.
        /// </summary>
        MiscellaneousIncome = 3,

        /// <summary>
        /// Specifies the expense for transportation.
        /// </summary>
        Transport = 4,

        /// <summary>
        /// Specifies the expense for Utilities.
        /// </summary>
        Utilities = 5,

        /// <summary>
        /// Specifies the expense for Groceries.
        /// </summary>
        Groceries = 6,

        /// <summary>
        /// Specifies the expense for Rent.
        /// </summary>
        Rent = 7,

        /// <summary>
        /// Specifies the expense for Food.
        /// </summary>
        Food = 8,

        /// <summary>
        /// Specifies the expense for Shopping.
        /// </summary>
        Shopping = 9,

        /// <summary>
        /// Specifies any Miscellaneous expense.
        /// </summary>
        MiscellaneousExpense = 10,
    }
}
