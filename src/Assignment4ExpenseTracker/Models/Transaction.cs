namespace Assignment4ExpenseTracker.Models
{
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Represents the transaction in the expense tracker.
    /// </summary>
    internal class Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction()
        {
            this.Id = Guid.NewGuid();
            this.Description = string.Empty;
            this.TimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="id">Unique identifier of the transaction.</param>
        public Transaction(Guid id)
        {
            this.Id = id;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class by copying the properties from another transaction.
        /// </summary>
        /// <param name="transaction">The transaction to be copied.</param>
        /// <exception cref="ArgumentNullException">Thrown when the transaction object is null.</exception>
        public Transaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            // Copy the read-only ID and other fields from the other transaction
            this.Id = transaction.Id;
            this.Amount = transaction.Amount;
            this.Type = transaction.Type;
            this.Category = transaction.Category;
            this.Method = transaction.Method;
            this.TimeStamp = transaction.TimeStamp;
            this.Description = transaction.Description;
        }

        /// <summary>
        /// Gets the unique identifier for the transaction.
        /// </summary>
        /// <value name="Id">Unique identifier of the transaction.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        /// <value name="Amount">Amount involved in the transaction.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the flow type of the transaction.
        /// </summary>
        /// <value name="Type">Type of the flow involved in transaction.</value>
        public FlowType Type { get; set; }

        /// <summary>
        /// Gets or sets the category of the transaction.
        /// </summary>
        /// <value name="Category">Category of the transaction.</value>
        public TransactionCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the method of the transaction.
        /// </summary>
        /// <value name="Method">Payment Method of the transaction.</value>
        public PaymentMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the transaction.
        /// </summary>
        /// <value name="TimeStamp">The date and time of the transaction.</value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the description for the transaction.
        /// </summary>
        /// <value name="Description">Optional Description of the transaction.</value>
        public string? Description { get; set; }
    }
}
