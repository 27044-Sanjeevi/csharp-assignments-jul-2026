namespace Assignment5ExpenseTrackerEnhanced.Persistence.Csv
{
    using Assignment5ExpenseTrackerEnhanced.Models;

    /// <summary>
    /// Defines contracts for serializing and deserializing Transaction records in CSV format.
    /// </summary>
    internal interface ITransactionCsvSerializer
    {
        /// <summary>
        /// Serializes a transaction into a CSV line string.
        /// </summary>
        /// <param name="transaction">The transaction object to serialize.</param>
        /// <returns>A CSV formatted line string.</returns>
        string Serialize(Transaction transaction);

        /// <summary>
        /// Deserializes a CSV line string back into a Transaction object.
        /// </summary>
        /// <param name="line">The CSV line string.</param>
        /// <returns>A populated Transaction object, or null if deserialization fails.</returns>
        Transaction? Deserialize(string line);
    }
}
