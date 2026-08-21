namespace Assignment5ExpenseTrackerEnhanced.Persistence.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Assignment5ExpenseTrackerEnhanced.Models;
    using Assignment5ExpenseTrackerEnhanced.Models.Enums;
    using Assignment5ExpenseTrackerEnhanced.Persistence;

    /// <summary>
    /// Handles persisting transaction records to a CSV file.
    /// </summary>
    internal class CsvFileRepository : IRepository
    {
        private const string CsvHeader = "Id,Amount,Type,Category,Method,Timestamp,Description";

        private readonly string _filePath;
        private readonly List<Transaction> _transactions;
        private readonly ITransactionCsvSerializer _csvSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFileRepository"/> class.
        /// </summary>
        /// <param name="filePath">The file path of the CSV storage.</param>
        /// <param name="csvSerializer">The serializer for formatting transaction records.</param>
        public CsvFileRepository(string filePath, ITransactionCsvSerializer csvSerializer)
        {
            this._filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this._csvSerializer = csvSerializer ?? throw new ArgumentNullException(nameof(csvSerializer));
            this._transactions = new List<Transaction>();
            this.Load();
        }

        /// <inheritdoc />
        public void Add(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            this._transactions.Add(new Transaction(transaction));
            this.Save();
        }

        /// <inheritdoc />
        public IEnumerable<Transaction> GetAll()
        {
            return this.CloneAll();
        }

        /// <inheritdoc />
        public Transaction? GetById(Guid id)
        {
            Transaction? transaction = this._transactions.FirstOrDefault(t => t.Id == id);
            return transaction == null ? null : new Transaction(transaction);
        }

        /// <inheritdoc />
        public bool Update(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            int index = this.GetIndexById(transaction.Id);
            if (index == -1)
            {
                return false;
            }

            this._transactions[index] = new Transaction(transaction);
            this.Save();
            return true;
        }

        /// <inheritdoc />
        public bool Delete(Guid id)
        {
            int index = this.GetIndexById(id);
            if (index == -1)
            {
                return false;
            }

            this._transactions.RemoveAt(index);
            this.Save();
            return true;
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByTransactionType(TransactionType type)
        {
            return this.GetAll()
                .Where(t => t.Type == type)
                .ToList();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category)
        {
            return this.GetAll()
                .Where(t => t.Category == category)
                .ToList();
        }

        /// <inheritdoc />
        public int GetTransactionCount()
        {
            return this._transactions.Count;
        }

        /// <summary>
        /// Centralised single-source lookup helper to find an internal array index by Guid.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>An integer representing the index of the transaction in the List.</returns>
        private int GetIndexById(Guid id)
        {
            return this._transactions.FindIndex(t => t.Id == id);
        }

        /// <summary>
        /// Creates a deep copy of all transactions.
        /// </summary>
        /// <returns>A list of cloned transactions.</returns>
        private IReadOnlyList<Transaction> CloneAll()
        {
            List<Transaction> clonedTransactions = new List<Transaction>();
            foreach (Transaction transaction in this._transactions)
            {
                if (transaction != null)
                {
                    clonedTransactions.Add(new Transaction(transaction));
                }
            }

            return clonedTransactions;
        }

        /// <summary>
        /// Loads transaction records from the CSV file.
        /// </summary>
        private void Load()
        {
            if (!File.Exists(this._filePath))
            {
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(this._filePath);
                if (lines.Length <= 1)
                {
                    return; // Header only or empty file
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    Transaction? transaction = this._csvSerializer.Deserialize(line);
                    if (transaction != null)
                    {
                        this._transactions.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load transactions from CSV: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Saves transaction records to the CSV file.
        /// </summary>
        private void Save()
        {
            try
            {
                List<string> lines = new List<string>()
                {
                    CsvHeader,
                };

                foreach (Transaction transaction in this._transactions)
                {
                    lines.Add(this._csvSerializer.Serialize(transaction));
                }

                string tempPath = this._filePath + ".tmp";
                File.WriteAllLines(tempPath, lines, Encoding.UTF8);

                if (File.Exists(this._filePath))
                {
                    File.Replace(tempPath, this._filePath, null);
                }
                else
                {
                    File.Move(tempPath, this._filePath);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save transactions to CSV: {ex.Message}", ex);
            }
        }
    }
}
