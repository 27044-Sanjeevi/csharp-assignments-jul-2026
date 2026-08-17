namespace Assignment5ExpenseTrackerEnhanced.Controller
{
    /// <summary>
    /// Defines contracts for the main application controller.
    /// </summary>
    internal interface ITransactionController
    {
        /// <summary>
        /// Processes a transaction menu selection and invokes the corresponding operation.
        /// </summary>
        /// <param name="choice">The menu option selected for transaction operations.</param>
        /// <returns>true if the user chose to exit; otherwise, false.</returns>
        bool HandleMenu(int choice);

        /// <summary>
        /// Adds a transaction using user-provided details.
        /// </summary>
        void Add();

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        void Update();

        /// <summary>
        /// Displays all the transactions in a table.
        /// </summary>
        void ViewAll();

        /// <summary>
        /// Deletes an existing transaction from the repository.
        /// </summary>
        void Delete();

        /// <summary>
        /// Searches and filters transactions by dynamic criteria.
        /// </summary>
        void Search();

        /// <summary>
        /// Sorts and displays transactions by dynamic criteria.
        /// </summary>
        void Sort();

        /// <summary>
        /// Filters transactions based on the selected filter type.
        /// </summary>
        void Filter();

        /// <summary>
        /// Generates and displays financial insights and reports.
        /// </summary>
        void DisplayReport();
    }
}
