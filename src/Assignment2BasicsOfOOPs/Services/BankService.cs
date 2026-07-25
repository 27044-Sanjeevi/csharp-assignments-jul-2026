namespace Assignment2BasicsOfOOPs.Services
{
    using System;
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;
    using Assignment2BasicsOfOOPs.Validation;

    /// <summary>
    /// Provides services for bank transaction operations.
    /// </summary>
    internal class BankService
    {
        private readonly BankValidation _bankValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankService"/> class
        /// </summary>
        /// <param name="bankValidator">Validator for bank-related operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when bankValidator is null.</exception>
        public BankService(BankValidation bankValidator)
        {
            this._bankValidator = bankValidator ?? throw new ArgumentNullException(nameof(bankValidator));
        }

        /// <summary>
        /// Creates a new bank account of the specified type with the provided account number and initial balance.
        /// </summary>
        /// <param name="accountType">The type of bank account to create.</param>
        /// <param name="accountNumber">The identifier for the bank account.</param>
        /// <param name="initialBalance">The initial deposit amount for the account.</param>
        /// <returns>A new instance of a bank account of the specified type.</returns>
        /// <exception cref="ArgumentException">Thrown when the initial balance is negative or the account type is not recognized.</exception>
        public BankAccount CreateAccount(BankAccountType accountType, string accountNumber, decimal initialBalance)
        {
            ArgumentNullException.ThrowIfNull(nameof(accountNumber));

            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial Balance cannot be negative.", nameof(initialBalance));
            }

            return accountType switch
            {
                BankAccountType.Savings => new SavingsAccount(accountNumber, initialBalance),
                BankAccountType.Checking => new CheckingAccount(accountNumber, initialBalance),
                _ => throw new ArgumentException($"Unknown Account Type")
            };
        }

        /// <summary>
        /// Deposits the specified amount into the bank account.
        /// </summary>
        /// <param name="account">The bank account.</param>
        /// <param name="amount">The deposit amount.</param>
        /// <returns>True if the deposit succeeded; otherwise, false.</returns>
        public bool Deposit(BankAccount account, decimal amount)
        {
            ArgumentNullException.ThrowIfNull(account);

            if (amount <= 0)
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

            ArgumentNullException.ThrowIfNull(account);

            if (account.AccountType == BankAccountType.Savings && this._bankValidator.IsValidAmount(amount))
            {
                errorMessage = "Withdrawal amount must be a positive value.";
                return false;
            }

            return account.Withdraw(amount, out errorMessage);
        }
    }
}
