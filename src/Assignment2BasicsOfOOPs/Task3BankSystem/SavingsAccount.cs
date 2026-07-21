namespace Assignment2BasicsOfOOPs.Task3BankSystem
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
        public override void Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                throw new BankSystemException("\nTry again." +
                    "\nThe amount to be Withdrawn exceeds the current Balance, " +
                    "you are allowed to withdraw within your current balance.\n");
            }

            this.Balance -= amount;
        }
    }
}
