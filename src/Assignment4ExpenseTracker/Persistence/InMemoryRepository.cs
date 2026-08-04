namespace Assignment4ExpenseTracker.Persistence
{
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Provides an in-memory repository for storing and managing transactions.
    /// </summary>
    internal class InMemoryRepository : IRepository
    {
        private readonly List<Transaction> _transactions;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository()
        {
            this._transactions = new List<Transaction>();
        }

        /// <inheritdoc />
        public void Add(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            this._transactions.Add(transaction);
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
            int index = this._transactions.FindIndex(t => t.Id == transaction.Id);
            if (index == -1)
            {
                return false;
            }

            this._transactions[index] = transaction;
            return true;
        }

        /// <inheritdoc />
        public bool Delete(Guid id)
        {
            int index = this._transactions.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return false;
            }

            this._transactions.RemoveAt(index);
            return true;
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByFlowType(FlowType type)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in this.GetAll())
            {
                if (transaction.Type == type)
                {
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in this.GetAll())
            {
                if (transaction.Category == category)
                {
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> CloneAll()
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
    }
}
