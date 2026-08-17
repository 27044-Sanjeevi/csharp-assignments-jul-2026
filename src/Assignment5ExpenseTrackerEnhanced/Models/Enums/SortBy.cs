namespace Assignment5ExpenseTrackerEnhanced.Models.Enums
{
    /// <summary>
    /// Specifies the fields by which transactions can be sorted.
    /// </summary>
    internal enum SortBy
    {
        /// <summary>
        /// Sort transactions by date and time.
        /// </summary>
        Date = 1,

        /// <summary>
        /// Sort transactions by amount.
        /// </summary>
        Amount = 2,

        /// <summary>
        /// Sort transactions by category.
        /// </summary>
        Category = 3,
    }
}
