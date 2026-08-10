namespace Assignment5ExpenseTrackerEnhanced.Persistence
{
    using System;
    using System.Collections.Generic;
    using Assignment5ExpenseTrackerEnhanced.Models;
    using Assignment5ExpenseTrackerEnhanced.Models.Enums;

    /// <summary>
    /// Defines data access contracts for financial transactions.
    /// </summary>
    internal interface IRepository
    {
        /// <summary>
        /// Stores a transaction object into the repository.
        /// </summary>
        /// <param name="transaction">The transaction object.</param>
        void Add(Transaction transaction);

        /// <summary>
        /// Extracts all transactions from the repository.
        /// </summary>
        /// <returns>All transaction records in the repository.</returns>
        IEnumerable<Transaction> GetAll();

        /// <summary>
        /// Locates a single transaction record using its unique identifier.
        /// </summary>
        /// <param name="id">Id of the transaction record.</param>
        /// <returns>The specific transaction record if found; otherwise null.</returns>
        Transaction? GetById(Guid id);

        /// <summary>
        /// Updates an existing transaction record in the repository.
        /// </summary>
        /// <param name="transaction">The Transaction object to be updated.</param>
        /// <returns>true if the element was discovered and updated successfully; otherwise false.</returns>
        bool Update(Transaction transaction);

        /// <summary>
        /// Deletes a record permanently from the persistence framework using its unique ID.
        /// </summary>
        /// <param name="id">Id of the transaction record to be updated.</param>
        /// <returns>true if the target record is deleted successfully; otherwise false.</returns>
        bool Delete(Guid id);

        /// <summary>
        /// Sorts transactions in ascending order by amount.
        /// </summary>
        /// <returns>A read-only list containing the sorted transactions.</returns>
        IReadOnlyList<Transaction> SortByAmount();

        /// <summary>
        /// Sorts transactions in chronological order.
        /// </summary>
        /// <returns>A read-only list of transactions sorted by date.</returns>
        IReadOnlyList<Transaction> SortByDate();

        /// <summary>
        /// Retrieves the count of transactions in the repository.
        /// </summary>
        /// <returns>An integer representing the count of transactions in the repository.</returns>
        int GetTransactionCount();

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
    }
}
