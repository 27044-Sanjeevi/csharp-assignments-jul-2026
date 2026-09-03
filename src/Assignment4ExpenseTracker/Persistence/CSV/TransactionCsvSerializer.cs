namespace Assignment4ExpenseTracker.Persistence.Csv
{
    using System.Globalization;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Handles format-specific serialization and deserialization of Transaction objects to/from CSV.
    /// </summary>
    internal class TransactionCsvSerializer : ITransactionCsvSerializer
    {
        private const int FieldCount = 6;

        /// <inheritdoc />
        public string Serialize(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1},{2},{3},{4},{5},{6}",
                transaction.Id,
                transaction.Amount,
                transaction.Type,
                transaction.Category,
                transaction.Method,
                transaction.Timestamp.ToString("o", CultureInfo.InvariantCulture),
                this.EscapeCsv(transaction.Description));
        }

        /// <inheritdoc />
        public Transaction? Deserialize(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            var fields = new List<string>();
            var field = string.Empty;
            var inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        field += '"';
                        i++; // Skip next quote
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(field);
                    field = string.Empty;
                }
                else
                {
                    field += c;
                }
            }

            fields.Add(field);

            if (fields.Count < FieldCount)
            {
                return null;
            }

            try
            {
                var id = Guid.Parse(fields[0]);
                var amount = decimal.Parse(fields[1], CultureInfo.InvariantCulture);
                var type = Enum.Parse<TransactionType>(fields[2]);
                var category = Enum.Parse<TransactionCategory>(fields[3]);
                var method = Enum.Parse<PaymentMethod>(fields[4]);
                var timestamp = DateTime.Parse(fields[5], null, DateTimeStyles.RoundtripKind);
                var description = fields.Count > FieldCount ? fields[FieldCount] : null;

                if (string.IsNullOrEmpty(description))
                {
                    description = null;
                }

                return new Transaction(id)
                {
                    Amount = amount,
                    Type = type,
                    Category = category,
                    Method = method,
                    Timestamp = timestamp,
                    Description = description,
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Escapes special characters for a CSV field.
        /// </summary>
        /// <param name="value">The raw string field value.</param>
        /// <returns>A formatted CSV value.</returns>
        private string EscapeCsv(string? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var containsComma = value.Contains(",");
            var containsQuote = value.Contains("\"");
            var containsNewline = value.Contains("\n") || value.Contains("\r");

            if (containsComma || containsQuote || containsNewline)
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }
    }
}
