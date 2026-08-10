namespace Assignment4ExpenseTracker.Controller
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
        bool HandleTransactionMenu(int choice);

        /// <summary>
        /// Adds a transaction using user-provided details.
        /// </summary>
        void AddTransaction();

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        void UpdateTransaction();

        /// <summary>
        /// Displays all the transactions in a table.
        /// </summary>
        void ViewAllTransactions();

        /// <summary>
        /// Deletes an existing transaction from the repository.
        /// </summary>
        void DeleteTransaction();

        /// <summary>
        /// Generates and displays financial insights and reports.
        /// </summary>
        void GenerateReport();
    }
}
