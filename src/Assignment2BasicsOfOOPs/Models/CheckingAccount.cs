namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Represents the Checking account derived from the base class Bank Account
    /// </summary>
    internal class CheckingAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class
        /// </summary>
        /// <param name="accountNumber">Account Number of the bank account</param>
        /// <param name="initialBalance">Initial Balance of the bank account</param>
        public CheckingAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
            // empty body, assignment takes place in the parent constructor
        }

        /// <summary>
        /// Withdraws (subtarcts) the given amount from the Checking Account
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the Savings account</param>
        /// <returns>True if the withdrawal is successful, otherwise false</returns>
        public override bool Withdraw(decimal amount)
        {
            this.Balance -= amount;
            return true; // checking account allows overdrafts (as per the Assignment requirements), so it always returns true
        }
    }
}
