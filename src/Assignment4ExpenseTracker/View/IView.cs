namespace Assignment4ExpenseTracker.View
{
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Services.Validation;

    /// <summary>
    /// Represents a contract for view layer in the Expense Tracker application.
    /// </summary>
    internal interface IView
    {
        /// <summary>
        /// Displays the main menu options for the Expense Tracker application.
        /// </summary>
        void DisplayMenu();

        /// <summary>
        /// Reads the transaction amount from the user input, ensuring it is a valid decimal value.
        /// </summary>
        /// <returns>The amount as a decimal.</returns>
        decimal GetTransactionAmount();

        /// <summary>
        /// Reads the updated transaction amount based on the specified existing amount.
        /// </summary>
        /// <param name="existingAmount">The current transaction amount.</param>
        /// <returns>The updated transaction amount.</returns>
        decimal GetTransactionAmountToUpdate(decimal existingAmount);

        /// <summary>
        /// Displays a message indicating that no transactions are available.
        /// </summary>
        void DisplayTransactionsNotFound();

        /// <summary>
        /// Displays a success message indicating that the transaction was updated successfully.
        /// </summary>
        void DisplayUpdateSuccessful();

        /// <summary>
        /// Displays the header for the deletion operation.
        /// </summary>
        void DisplayDeleteHeader();

        /// <summary>
        /// Reads the flow type of the transaction.
        /// </summary>
        /// <returns>The flow type chosen by the user.</returns>
        FlowType GetFlowChoice();

        /// <summary>
        /// Reads the current payment method.
        /// </summary>
        /// <returns>The payment method chosen by the user.</returns>
        PaymentMethod GetPaymentMethod();

        /// <summary>
        /// Retrieves the income category of the transaction.
        /// </summary>
        /// <returns>The income category as a TransactionCategory.</returns>
        TransactionCategory GetIncomeCategory();

        /// <summary>
        /// Retrieves the expense category associated with a transaction.
        /// </summary>
        /// <returns>The transaction category for the expense.</returns>
        TransactionCategory GetExpenseCategory();

        /// <summary>
        /// Retrieves the parameter for filtering the transactions.
        /// </summary>
        /// <returns>The parameter to be filtered by.</returns>
        FilterType GetFilterTypeChoice();

        /// <summary>
        /// Reads the optional description of the transaction from user input.
        /// </summary>
        /// <returns>The description of the transaction as a string.</returns>
        string? GetTransactionDescription();

        /// <summary>
        /// Retrieves an updated transaction description based on the provided existing description.
        /// </summary>
        /// <param name="existingDescription">The current transaction description to update.</param>
        /// <returns>The updated transaction description, or null if no update is required.</returns>
        string? GetTransactionDescriptionToUpdate(string? existingDescription);

        /// <summary>
        /// Prints validation errors to the output display window if any exist.
        /// </summary>
        /// <param name="result">The validation result object to process.</param>
        void DisplayValidationResult(ValidationResult result);

        /// <summary>
        /// Prompts the user to select a row number within the specified range.
        /// index.
        /// </summary>
        /// <param name="maxIndex">The maximum valid row number for selection.</param>
        /// <returns>The zero-based index of the selected row.</returns>
        int GetIndexFromTable(int maxIndex);

        /// <summary>
        /// Displays a table of transactions after applying filters.
        /// </summary>
        /// <param name="transactions">The filtered list of transactions to display.</param>
        void DisplayFilteredTable(IReadOnlyList<Transaction> transactions);

        /// <summary>
        /// Renders a collection of transaction records as a formatted table grid.
        /// </summary>
        /// <param name="transactions">The collection of transaction data objects to display.</param>
        void DisplayAsTable(IReadOnlyList<Transaction> transactions);
    }
}
