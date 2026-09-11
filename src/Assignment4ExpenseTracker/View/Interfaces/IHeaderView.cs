namespace Assignment4ExpenseTracker.View.Interfaces
{
    /// <summary>
    /// Defines the contract for the header methods.
    /// </summary>
    internal interface IHeaderView
    {
        /// <summary>
        /// Displays the header for the deletion operation.
        /// </summary>
        void DisplayDeleteHeader();

        /// <summary>
        /// Displays the header for the add operation.
        /// </summary>
        void DisplayAddHeader();

        /// <summary>
        /// Displays the header for the update operation.
        /// </summary>
        void DisplayUpdateHeader();

        /// <summary>
        /// Displays the header for the search operation.
        /// </summary>
        void DisplaySearchHeader();

        /// <summary>
        /// Displays the header for the filter operation.
        /// </summary>
        void DisplayFilterHeader();

        /// <summary>
        /// Displays the header for the report operation.
        /// </summary>
        void DisplayReportHeader();

        /// <summary>
        /// Displays the header for the view all operation.
        /// </summary>
        void DisplayAllTransactionsHeader();
    }
}
