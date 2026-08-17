namespace Assignment5ExpenseTrackerEnhanced.Models.Enums
{
    /// <summary>
    /// Specifies the category of transaction including income and expense.
    /// </summary>
    internal enum TransactionCategory
    {
        // Income Sources

        /// <summary>
        /// Specifies the income through salary.
        /// </summary>
        Salary = 1,

        /// <summary>
        /// Specifies the income through Investments.
        /// </summary>
        Investment = 2,

        /// <summary>
        /// Specifies the income through Freelance work.
        /// </summary>
        Freelance = 3,

        /// <summary>
        /// Specifies the income through Business operations.
        /// </summary>
        Business = 4,

        /// <summary>
        /// Specifies the income through Gifts.
        /// </summary>
        Gifts = 5,

        /// <summary>
        /// Specifies any Miscellaneous income.
        /// </summary>
        MiscellaneousIncome = 6,

        // Expense Categories

        /// <summary>
        /// Specifies the expense for transportation.
        /// </summary>
        Transport = 7,

        /// <summary>
        /// Specifies the expense for Utilities.
        /// </summary>
        Utilities = 8,

        /// <summary>
        /// Specifies the expense for Groceries.
        /// </summary>
        Groceries = 9,

        /// <summary>
        /// Specifies the expense for Rent.
        /// </summary>
        Rent = 10,

        /// <summary>
        /// Specifies the expense for Food.
        /// </summary>
        Food = 11,

        /// <summary>
        /// Specifies the expense for Shopping.
        /// </summary>
        Shopping = 12,

        /// <summary>
        /// Specifies the expense for Healthcare.
        /// </summary>
        Healthcare = 13,

        /// <summary>
        /// Specifies the expense for Education.
        /// </summary>
        Education = 14,

        /// <summary>
        /// Specifies any Miscellaneous expense.
        /// </summary>
        MiscellaneousExpense = 15,
    }
}
