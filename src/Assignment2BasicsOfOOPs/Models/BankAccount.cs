namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Represents the base class for a bank account.
    /// </summary>
    internal abstract class BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">Account number of the bank account.</param>
        /// <param name="initialBalance">Initial Balance of the bank account.</param>
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = initialBalance;
        }

        /// <summary>
        /// Gets or sets the Account Number of the bank account.
        /// </summary>
        /// <value>Account number of the bank account.</value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the Balance in the bank account.
        /// </summary>
        /// <value>Balance in the bank account.</value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Deposits (adds) the given amount to the bank account balance.
        /// </summary>
        /// <param name="amount">Amount to be deposited to the account.</param>
        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraws (subtracts) the given amount from the bank account balance.
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the account.</param>
        /// <param name="errorMessage">Error message output if validation fails.</param>
        /// <returns>True if withdrawal succeeds; otherwise, false.</returns>
        public abstract bool Withdraw(decimal amount, out string errorMessage);

        /// <summary>
        /// Formats and returns all bank account details.
        /// </summary>
        /// <returns>A formatted string of account details.</returns>
        public virtual string GetDetails() => $"Account Number: {this.AccountNumber}\n" +
                                              $"Current Balance: Rs. {this.Balance:F2}\n";

        /// <summary>
        /// Formats and returns only the current balance.
        /// </summary>
        /// <returns>A formatted string of the current balance.</returns>
        public string GetBalanceDetails() => $"Current Balance: Rs. {this.Balance:F2}\n";
    }
}
