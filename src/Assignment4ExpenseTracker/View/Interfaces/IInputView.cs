namespace Assignment4ExpenseTracker.View.Interfaces
{
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Defines the contract for the input methods.
    /// </summary>
    internal interface IInputView
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
        /// Prompts the user to confirm deletion of the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction record to be deleted.</param>
        /// <returns>True if the user confirmed deletion; otherwise, false.</returns>
        bool ConfirmDelete(Transaction transaction);

        /// <summary>
        /// Reads the flow type of the transaction.
        /// </summary>
        /// <param name="existingFlow">Holds the existing Flow type of the transaction.</param>
        /// <returns>The flow type chosen by the user.</returns>
        TransactionType GetTransactionTypeChoice(TransactionType? existingFlow = null);

        /// <summary>
        /// Retrieves the parameter for filtering the transactions.
        /// </summary>
        /// <param name="existingType">Holds the existing Filter type of the transaction.</param>
        /// <returns>The parameter to be filtered by.</returns>
        FilterType GetFilterTypeChoice(FilterType? existingType = null);

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
        /// Retrieves the Date and time of thr transaction.
        /// </summary>
        /// <param name="existingDateTime">The existing date and time of transaction.</param>
        /// <param name="isOptional">true if date is optional;otherwise false.</param>
        /// <returns>A DateTime object represeting the date and time of the transaction.</returns>
        DateTime? GetDateTime(DateTime? existingDateTime = null, bool isOptional = false);

        /// <summary>
        /// Prompts the user to select a row number within the specified range.
        /// index.
        /// </summary>
        /// <param name="maxIndex">The maximum valid row number for selection.</param>
        /// <returns>The zero-based index of the selected row.</returns>
        int GetIndexFromTable(int maxIndex);

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
    }
}
