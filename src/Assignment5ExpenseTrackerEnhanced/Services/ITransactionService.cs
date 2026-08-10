namespace Assignment5ExpenseTrackerEnhanced.Services
{
    using System;
    using System.Collections.Generic;
    using Assignment5ExpenseTrackerEnhanced.Models;
    using Assignment5ExpenseTrackerEnhanced.Models.DTOs;
    using Assignment5ExpenseTrackerEnhanced.Models.Enums;
    using Assignment5ExpenseTrackerEnhanced.Services.Validation;

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
        ValidationResult UpdateTransaction(TransactionUpdateDto transaction);

        /// <summary>
        /// Sorts transactions in ascending order by amount.
        /// </summary>
        /// <returns>A read-only list containing the sorted transactions.</returns>
        IReadOnlyList<Transaction> SortTransactionsByAmount();

        /// <summary>
        /// Filters transactions based on the specified flow type.
        /// </summary>
        /// <param name="type">The type to filter.</param>
        /// <returns>An enumerable collection of transactions that match the flow type criteria.</returns>
        IReadOnlyList<Transaction> FilterByTransactionType(TransactionType type);

        /// <summary>
        /// Filters transactions based on the specified category.
        /// </summary>
        /// <param name="category">The category to filter.</param>
        /// <returns>An enumerable collection of transactions that match the category criteria.</returns>
        IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category);

        /// <summary>
        /// Extracts all transaction records currently stored in the system.
        /// </summary>
        /// <returns>A validation result object containing the outcome and explanation.</returns>
        IReadOnlyList<Transaction> GetAllTransactions();

        /// <summary>
        /// Generates a financial report DTO containing summary metrics.
        /// </summary>
        /// <returns>A ReportDto containing consolidated statistics.</returns>
        ReportDto GenerateFinancialReport();
    }
}
