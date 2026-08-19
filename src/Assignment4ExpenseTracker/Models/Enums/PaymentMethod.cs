namespace Assignment4ExpenseTracker.Models.Enums
{
    /// <summary>
    /// Specifies the method of payment in a transaction.
    /// </summary>
    internal enum PaymentMethod
    {
        /// <summary>
        /// Specifies the transaction by cash.
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Specifies the transaction by Credit Card.
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// Specifies the transaction by Debit Card.
        /// </summary>
        DebitCard = 3,

        /// <summary>
        /// Specifies the transaction by Bank Transfer.
        /// </summary>
        BankTransfer = 4,
    }
}
