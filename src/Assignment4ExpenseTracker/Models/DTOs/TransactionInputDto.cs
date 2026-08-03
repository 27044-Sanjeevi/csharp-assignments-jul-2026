namespace Assignment4ExpenseTracker.Models.DTOs
{
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Serves as a data transfer object for handling transaction inputs across application tiers.
    /// </summary>
    internal class TransactionInputDto
    {
        /// <summary>
        /// Gets the amount involved in the transaction.
        /// </summary>
        /// <value name="Amount">Amount involved in the transaction.</value>
        public decimal Amount { get; init; }

        /// <summary>
        /// Gets the flow type of the transaction.
        /// </summary>
        /// <value name="Type">Type of the flow involved in transaction.</value>
        public FlowType Type { get; init; }

        /// <summary>
        /// Gets the category of the transaction.
        /// </summary>
        /// <value name="Category">Category of the transaction.</value>
        public TransactionCategory Category { get; init; }

        /// <summary>
        /// Gets the method of the transaction.
        /// </summary>
        /// <value name="Method">Payment Method of the transaction.</value>
        public PaymentMethod Method { get; init; }

        /// <summary>
        /// Gets the description for the transaction.
        /// </summary>
        /// <value name="Description">Optional Description of the transaction.</value>
        public string? Description { get; init; }
    }
}
