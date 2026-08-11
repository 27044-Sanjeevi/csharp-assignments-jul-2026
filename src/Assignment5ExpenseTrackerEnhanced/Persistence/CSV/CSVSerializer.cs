using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.DTOs;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;
using Spectre.Console;

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
            return $"{transaction.Id}," +
                $"{transaction.Amount}," +
                $"{transaction.Type}," +
                $"{transaction.Category}," +
                $"{transaction.Method}," +
                $"{transaction.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}," +
                $"{transaction.Description}";
        }

        /// <inheritdoc />
        public Transaction DeSerialize(string csvText)
        {
            string[] fields = csvText.Split(',');

            TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
            {
                Id = Guid.Parse(fields[0]),
                Amount = decimal.Parse(fields[1]),
                Type = (TransactionType)Enum.Parse(typeof(TransactionType), fields[2]),
                Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), fields[3]),
                Method = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), fields[4]),
                TimeStamp = DateTime.Parse(fields[5]),
                Description = fields[6],
            };

            return new Transaction(transactionUpdateDto);
        }
    }
}
