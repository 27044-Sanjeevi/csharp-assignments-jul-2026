using Assignment5ExpenseTrackerEnhanced.Models;

namespace Assignment5ExpenseTrackerEnhanced.Persistence.CSV
{
    /// <summary>
    /// Provides functionality to serialize Transaction objects into CSV format.
    /// </summary>
    internal class CSVSerializer : ICSVSerializer
    {
        /// <inheritdoc />
        public string Serialize(Transaction transaction)
        {
            return $"{transaction.Id}, " +
                $"{transaction.Amount}, " +
                $"{transaction.Type}, " +
                $"{transaction.Category}, " +
                $"{transaction.Method}, " +
                $"{transaction.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}, " +
                $"{transaction.Description}";
        }

        public Transaction DeSerialize(string csv)
        {
            List<string> fields = csv.Split(',');

            return new Transaction
            {
                Id = int.fields[0],
                Amount = fields[1],
                Type = fields[0],
                Category = fields[1],
                Method = fields[0],
                TimeStamp = fields[1],
                Description = fields[0],
            };
        }
    }
}
