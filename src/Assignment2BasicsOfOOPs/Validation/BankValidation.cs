namespace Assignment2BasicsOfOOPs.Validation
{
    /// <summary>
    /// Represents the bank related Validations
    /// </summary>
    internal class BankValidation
    {
        /// <summary>
        /// Validates the amount to be positive
        /// </summary>
        /// <param name="amount">The amount to be validated.</param>
        /// <returns>True if amount is positive, else false.</returns>
        public bool IsValidAmount(decimal amount) => amount > 0;

        /// <summary>
        /// Determines whether the specified account number is not null or empty.
        /// </summary>
        /// <param name="accountNumber">The account number to validate.</param>
        /// <returns>true if the account number is not null or empty; otherwise, false.</returns>
        public bool IsValidAccountNumber(string accountNumber) => !string.IsNullOrEmpty(accountNumber);
    }
}
