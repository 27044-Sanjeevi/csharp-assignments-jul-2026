using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;

namespace Assignment5ExpenseTrackerEnhanced.Persistence.CSV
{
    /// <summary>
    /// Provides a CSV repository for storing and managing transactions.
    /// </summary>
    internal class CSVRepository : IRepository, ICSVSerializer
    {
        private readonly string _filePath;
        private string _csvHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSVRepository"/> class.
        /// </summary>
        /// <param name="filePath">String containing the relative path of the CSV repository.</param>
        /// <param name="csvHeader">String containing the header for the CSV repository.</param>
        public CSVRepository(string filePath, string csvHeader)
        {
            this._filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this._csvHeader = csvHeader ?? throw new ArgumentNullException(nameof(csvHeader));

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, this._csvHeader);
            }
        }

        void Add(Transaction transaction)
        {
            string csvFormat = 
        }

        bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<Transaction> IRepository.FilterByCategory(TransactionCategory category)
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<Transaction> IRepository.FilterByTransactionType(TransactionType type)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Transaction> IRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Transaction? IRepository.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        int IRepository.GetTransactionCount()
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<Transaction> IRepository.SortByAmount()
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<Transaction> IRepository.SortByDate()
        {
            throw new NotImplementedException();
        }

        bool IRepository.Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
