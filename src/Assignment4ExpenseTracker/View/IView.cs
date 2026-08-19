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
        /// Displayes a success message on successful deletion.
        /// </summary>
        void DisplayDeleteSuccessful();

        /// <summary>
        /// Prompts the user to confirm deletion of the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction record to be deleted.</param>
        /// <returns>True if the user confirmed deletion; otherwise, false.</returns>
        bool ConfirmDelete(Transaction transaction);

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
        /// Displays the header for the report operation.
        /// </summary>
        void DisplayReportHeader();

        /// <summary>
        /// Displays the header for the view all operation.
        /// </summary>
        void DisplayAllTransactionsHeader();

        /// <summary>
        /// Reads the flow type of the transaction.
        /// </summary>
        /// <param name="existingFlow">Holds the existing Flow type of the transaction.</param>
        /// <returns>The flow type chosen by the user.</returns>
        TransactionType GetTransactionTypeChoice(TransactionType? existingFlow = null);

        /// <summary>
        /// Reads the current payment method.
        /// </summary>
        /// <param name="existingMethod">Holds the existing Payment Method of the transaction.</param>
        /// <returns>The payment method chosen by the user.</returns>
        PaymentMethod GetPaymentMethod(PaymentMethod? existingMethod = null);

        /// <summary>
        /// Retrieves the income category of the transaction.
        /// </summary>
        /// <param name="existingCategory">Holds the exiting category of the transaction.</param>
        /// <returns>The income category as a TransactionCategory.</returns>
        TransactionCategory GetIncomeCategory(TransactionCategory? existingCategory = null);

        /// <summary>
        /// Retrieves the expense category associated with a transaction.
        /// </summary>
        /// <param name="existingCategory">Holds the exiting category of the transaction.</param>
        /// <returns>The transaction category for the expense.</returns>
        TransactionCategory GetExpenseCategory(TransactionCategory? existingCategory = null);

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
        /// Displays Message on successful addition of transaction.
        /// </summary>
        void DisplayAddSuccessful();

        /// <summary>
        /// Prompts the user to select a row number within the specified range.
        /// index.
        /// </summary>
        /// <param name="maxIndex">The maximum valid row number for selection.</param>
        /// <returns>The zero-based index of the selected row.</returns>
        int GetIndexFromTable(int maxIndex);

        /// <summary>
        /// Renders a collection of transaction records as a formatted table grid.
        /// </summary>
        /// <param name="transactions">The collection of transaction data objects to display.</param>
        void DisplayAsTable(IReadOnlyList<Transaction> transactions);

        /// <summary>
        /// Displays the details of a given transaction.
        /// </summary>
        /// <param name="transaction">The transaction object containing the details to be displayed.</param>
        void DisplayTransactionDetails(Transaction transaction);

        /// <summary>
        /// Displays financial insights and summary statistics.
        /// </summary>
        /// <param name="totalIncome">Total income amount.</param>
        /// <param name="totalExpense">Total expense amount.</param>
        /// <param name="netBalance">Net balance (income - expense).</param>
        /// <param name="totalTransactions">Count of all transactions.</param>
        void DisplayInsights(decimal totalIncome, decimal totalExpense, decimal netBalance, int totalTransactions);

        /// <summary>
        /// Displays the main menu with operations descriptions table.
        /// </summary>
        void ShowMainMenu();

        /// <summary>
        /// Clears the console screen.
        /// </summary>
        void ClearScreen();

        /// <summary>
        /// Prompts the user to make a menu selection.
        /// </summary>
        /// <returns>An enum specifying the menu option selected.</returns>
        MainMenuOption ReadChoice();

        /// <summary>
        /// Pauses execution and prompts the user to return to the main screen.
        /// </summary>
        void PauseAndReturn();

        /// <summary>
        /// Displays a selection menu using arrow keys and returns the selected item itself.
        /// </summary>
        /// <typeparam name="T">The type of the choices.</typeparam>
        /// <param name="title">The title prompt for selection.</param>
        /// <param name="choices">The list of choices to display.</param>
        /// <param name="displaySelector">Optional function to format how each choice is displayed as text.</param>
        /// <returns>The selected item.</returns>
        T ReadSelection<T>(string title, IEnumerable<T> choices, Func<T, string>? displaySelector = null);

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        void HandleError(string message);
    }
}
