namespace Assignment4ExpenseTracker.Persistence
{
    using System;
    using System.Collections.Generic;
    using Assignment4ExpenseTracker.Models;

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
        /// <returns>the specific transaction record if found; otherwise null.</returns>
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
    }
}
