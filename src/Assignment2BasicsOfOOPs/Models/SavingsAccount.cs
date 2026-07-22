namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Represents the savings account in a bank system, derived from BankAccount
    /// </summary>
    internal class SavingsAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsAccount"/> class
        /// </summary>
        /// <param name="accountNumber">Account Number of the bank account</param>
        /// <param name="initialBalance">Initial Balance of the bank account</param>
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
            // empty body, assignment takes place in the parent constructor
        }

        /// <summary>
        /// Withdraws (subtarcts) the given amount from the Savings Account
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the Savings account</param>
        /// <returns>True if the withdrawal is successful, otherwise false</returns>
        public override bool Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                return false;
            }

            this.Balance -= amount;
            return true;
        }
    }
}
