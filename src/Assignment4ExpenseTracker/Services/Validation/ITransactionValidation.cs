namespace Assignment4ExpenseTracker.Services.Validation
{
    using Assignment4ExpenseTracker.Models;

    /// <summary>
    /// Specifies the contract for validation of the transactions.
    /// </summary>
    internal interface ITransactionValidation
    {
        /// <summary>
        /// Validates the given transaction object to ensure it meets the required criteria.
        /// </summary>
        /// <param name="transaction">The transaction object to be validated.</param>
        /// <returns>The validation result object.</returns>
        ValidationResult ValidateTransaction(Transaction transaction);

        /// <summary>
        /// Validates a deletion request based on the transaction record identifier.
        /// </summary>
        /// <param name="id">The transaction identifier to evaluate.</param>
        /// <returns>A validation result container.</returns>
        ValidationResult ValidateDeletion(Guid id);
    }
}
