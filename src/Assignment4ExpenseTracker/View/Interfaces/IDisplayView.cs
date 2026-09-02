namespace Assignment4ExpenseTracker.View.Interfaces
{
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Services.Validation;

    /// <summary>
    /// Defines the contract for display methods.
    /// </summary>
    internal interface IDisplayView
    {
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
        /// Retrieves the keyword to be searched across all the transactions from the user.
        /// </summary>
        /// <returns>The string containing the keyword to be searched.</returns>
        string GetSearchKeyword();

        /// <summary>
        /// Reads the optional description of the transaction from user input.
        /// </summary>
        /// <returns>The description of the transaction as a string.</returns>
        string? GetTransactionDescription();

        /// <summary>
        /// Collects sorting criteria from the user.
        /// </summary>
        /// <returns>A tuple containing the SortBy and SortOrder enums.</returns>
        (SortBy sortBy, SortOrder order) GetSortingCriteria();

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
        /// Displays the main menu with operations descriptions table.
        /// </summary>
        void ShowMainMenu();

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        void HandleError(string message);

        /// <summary>
        /// Clears the console screen.
        /// </summary>
        void ClearScreen();
    }
}
