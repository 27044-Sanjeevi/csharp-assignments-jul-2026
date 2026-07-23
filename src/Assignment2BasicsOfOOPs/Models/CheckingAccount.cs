namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Represents the checking account derived from the base class Bank Account.
    /// </summary>
    internal class CheckingAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">Account Number of the bank account.</param>
        /// <param name="initialBalance">Initial Balance of the bank account.</param>
        public CheckingAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        /// <summary>
        /// Withdraws (subtracts) the given amount from the Checking Account.
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the Checking account.</param>
        /// <param name="errorMessage">Output error message (always empty for checking).</param>
        /// <returns>Always true since Checking Accounts allow arbitrary withdrawals/overdrafts.</returns>
        public override bool Withdraw(decimal amount, out string errorMessage)
        {
            errorMessage = string.Empty;
            this.Balance -= amount;
            return true; // always returns true, since overdraft is allowed
        }
    }
}
