using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;
using Assignment5ExpenseTrackerEnhanced.Persistence;

namespace Assignment5ExpenseTrackerEnhanced.Persistence.CSV
{
    /// <summary>
    /// Provides a CSV repository for storing and managing transactions.
    /// </summary>
    internal class CSVRepository : IRepository
    {
        private readonly string _filePath;
        private string _csvHeader;
        private ICSVSerializer _csvSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSVRepository"/> class.
        /// </summary>
        /// <param name="filePath">String containing the relative path of the CSV repository.</param>
        /// <param name="csvHeader">String containing the header for the CSV repository.</param>
        /// <param name="csvSerializer">Serializer for handling with CSV file.</param>
        public CSVRepository(string filePath, string csvHeader, ICSVSerializer csvSerializer)
        {
            this._filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this._csvHeader = csvHeader ?? throw new ArgumentNullException(nameof(csvHeader));
            this._csvSerializer = csvSerializer ?? throw new ArgumentNullException(nameof(csvSerializer));
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, this._csvHeader);
            }
        }

        /// <inheritdoc />
        public void Add(Transaction transaction)
        {
            string csvText = this._csvSerializer.Serialize(transaction);
            File.AppendAllText(this._filePath, csvText + Environment.NewLine);
        }

        /// <inheritdoc />
        public bool Delete(Guid id)
        {
            string tempFilePath = this._filePath + ".tmp";
            IEnumerable<Transaction> transactions = this.GetAll().ToList();

            Transaction? transactionToRemove = transactions.FirstOrDefault(t => t.Id == id);
            if (transactionToRemove == null)
            {
                return false;
            }

            string[] csvText;
            List<string> lines = new List<string>();

            foreach (Transaction transaction in transactions)
            {
                lines.Add();
            }

            File.WriteAllLines(tempFilePath, csvText);
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByTransactionType(TransactionType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<Transaction> GetAll()
        {
            List<Transaction> result = new List<Transaction>();

            if (!File.Exists(this._filePath))
            {
                return result;
            }

            string[] content = File.ReadAllLines(this._filePath);
            foreach (string row in content)
            {
                result.Add(this._csvSerializer.DeSerialize(row));
            }

            return result;
        }

        /// <inheritdoc />
        public Transaction? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetTransactionCount()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> SortByAmount()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> SortByDate()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IRepository.Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
