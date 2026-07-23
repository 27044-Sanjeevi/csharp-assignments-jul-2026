namespace Assignment2BasicsOfOOPs.Services
{
    using System;
    using Assignment2BasicsOfOOPs.Models;

    /// <summary>
    /// Provides services for bank transaction operations.
    /// </summary>
    internal class BankService
    {
        /// <summary>
        /// Deposits the specified amount into the bank account.
        /// </summary>
        /// <param name="account">The bank account.</param>
        /// <param name="amount">The deposit amount.</param>
        /// <returns>True if the deposit succeeded; otherwise, false.</returns>
        public bool Deposit(BankAccount account, decimal amount)
        {
            if (account == null || amount <= 0)
            {
                return false;
            }

            account.Deposit(amount);
            return true;
        }

        /// <summary>
        /// Withdraws the specified amount from the bank account.
        /// </summary>
        /// <param name="account">The bank account.</param>
        /// <param name="amount">The withdrawal amount.</param>
        /// <param name="errorMessage">Output error details if validation fails.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        public bool Withdraw(BankAccount account, decimal amount, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (account == null)
            {
                errorMessage = "Account cannot be null.";
                return false;
            }

            if (amount <= 0)
            {
                errorMessage = "Withdrawal amount must be a positive value.";
                return false;
            }

            return account.Withdraw(amount, out errorMessage);
        }
    }
}
