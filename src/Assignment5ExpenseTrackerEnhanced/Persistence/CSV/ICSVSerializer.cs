using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.DTOs;

namespace Assignment5ExpenseTrackerEnhanced.Persistence.CSV
{
    /// <summary>
    /// Defines a contract for serializing objects to CSV format.
    /// </summary>
    internal interface ICSVSerializer
    {
        /// <summary>
        /// Serializes a Transaction object to its CSV representation.
        /// </summary>
        /// <param name="transaction">The Transaction instance to serialize.</param>
        /// <returns>A string containing the serialized Transaction.</returns>
        string Serialize(Transaction transaction);

        /// <summary>
        /// Deserializes the specified string in CSV format into its original object representation.
        /// </summary>
        /// <param name="csv">The serialized string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        Transaction DeSerialize(string csv);
    }
}
