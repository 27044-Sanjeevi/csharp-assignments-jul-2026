namespace Assignment4ExpenseTracker.Models.Enums
{
    /// <summary>
    /// Specifies the category of the expense in a Transaction.
    /// </summary>
    internal enum ExpenseCategory
    {
        /// <summary>
        /// Specifies the expense for transportation.
        /// </summary>
        Transport = 1,

        /// <summary>
        /// Specifies the expense for Utilities.
        /// </summary>
        Utilities = 2,

        /// <summary>
        /// Specifies the expense for Groceries.
        /// </summary>
        Groceries = 3,

        /// <summary>
        /// Specifies the expense for Rent.
        /// </summary>
        Rent = 4,

        /// <summary>
        /// Specifies the expense for Food.
        /// </summary>
        Food = 5,

        /// <summary>
        /// Specifies the expense for Shopping.
        /// </summary>
        Shopping = 6,

        /// <summary>
        /// Specifies any Miscellaneous expense.
        /// </summary>
        Miscellaneous = 7,
    }
}
