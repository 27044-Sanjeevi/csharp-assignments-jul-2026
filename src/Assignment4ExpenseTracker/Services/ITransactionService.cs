namespace Assignment4ExpenseTracker.Services
{
    using System;
    using System.Collections.Generic;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.DTOs;
    using Assignment4ExpenseTracker.Services.Validation;

    /// <summary>
    /// Defines business logic contracts for managing financial transactions.
    /// </summary>
    internal interface ITransactionService
    {
        /// <summary>
        /// Creates a new transaction using the specified transaction details.
        /// </summary>
        /// <param name="transactionDto">The transaction data to be created.</param>
        /// <returns>A ValidationResult indicating whether the transaction creation was successful.</returns>
        ValidationResult CreateTransaction(TransactionInputDto transactionDto);

        /// <summary>
        /// Deletes an existing transaction from the repository using its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction record to be deleted.</param>
        /// <returns>A validation result object containing the outcome and explanation.</returns>
        ValidationResult DeleteTransaction(Guid id);

        /// <summary>
        /// Updates the details of an existing transaction record within the repository.
        /// </summary>
        /// <param name="transaction">The transaction object containing the updated records.</param>
        /// <returns>A validation result object containing the outcome and explanation.</returns>
        ValidationResult UpdateTransaction(Transaction transaction);

        /// <summary>
        /// Extracts all transaction records currently stored in the system.
        /// </summary>
        /// <returns>A validation result object containing the outcome and explanation.</returns>
        List<Transaction> GetAllTransactions();
    }
}
