namespace Assignment2BasicsOfOOPs.Task3BankSystem
{
    /// <summary>
    /// Represents the base class for bank account
    /// </summary>
    internal abstract class BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class
        /// </summary>
        /// <param name="accountNumber">Account number of the bank account</param>
        /// <param name="initialBalance">Initial Balance of the bank account</param>
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = initialBalance;
        }

        /// <summary>
        /// Gets or sets the Account Number of the bank account
        /// </summary>
        /// <value name="AccountNumber">Account number of the bank account</value>
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the Balance in the bank account
        /// </summary>
        /// <value name="Balance">Account number of the bank account</value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Deposits (adds) the given amount to the bank account balance
        /// </summary>
        /// <param name="amount">Amount to be deposited to the account</param>
        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraws (subtarcts) the given amount from the bank account balance
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the account</param>
        public abstract void Withdraw(decimal amount);

        /// <summary>
        /// Prints both Account Number and Current Balance
        /// </summary>
        public void PrintAllDetails()
        {
            ConsoleIO.Write($"Account Number: {this.AccountNumber}\n" +
                            $"Current Balance: {this.Balance}\n");
        }

        /// <summary>
        /// Prints only the current Balance
        /// </summary>
        public void PrintCurrentBalance()
        {
            ConsoleIO.Write($"Current Balance: {this.Balance}\n");
        }
    }
}
