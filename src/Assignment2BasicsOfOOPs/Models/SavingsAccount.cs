namespace Assignment2BasicsOfOOPs.Models
{
    using Assignment2BasicsOfOOPs.Models.Enums;

    /// <summary>
    /// Represents the savings account in a bank system, derived from BankAccount.
    /// </summary>
    internal class SavingsAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">Account Number of the bank account.</param>
        /// <param name="initialBalance">Initial Balance of the bank account.</param>
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance, BankAccountType.Savings)
        {
        }

        /// <summary>
        /// Withdraws (subtracts) the given amount from the Savings Account.
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the Savings account.</param>
        /// <param name="errorMessage">Output error message if balance is exceeded.</param>
        /// <returns>True if withdrawal succeeds; false if insufficient funds.</returns>
        public override bool Withdraw(decimal amount, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (amount > this.Balance)
            {
                errorMessage = "The amount to be Withdrawn exceeds the current Balance. You are allowed to withdraw within your current balance.";
                return false;
            }

            this.Balance -= amount;
            return true;
        }
    }
}
