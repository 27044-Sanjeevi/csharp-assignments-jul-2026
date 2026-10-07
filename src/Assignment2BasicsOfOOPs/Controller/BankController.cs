namespace Assignment2BasicsOfOOPs.Controller
{
    using System.Security.Principal;
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;
    using Assignment2BasicsOfOOPs.Services;
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Controller class for handling bank-related operations and interactions between the view and the service layer.
    /// </summary>
    internal class BankController
    {
        private const int MinBankChoice = 1;
        private const int MaxBankChoice = 4;

        private readonly ConsoleView _view;
        private readonly BankService _bankService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="bankService">The bank related services.</param>
        public BankController(ConsoleView view, BankService bankService)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        }

        /// <summary>
        /// Prompts the user and runs Task 3 (Bank System) interactively.
        /// </summary>
        public void RunBankTask()
        {
            this._view.PrintHeader("Task 3: Bank System");

            // 1. Create Bank Account
            this._view.PrintSubHeader("Account Setup\n" +
                                      "1. Savings Account (Requires positive balance, overdraft not allowed)\n" +
                                      "2. Checking Account (Allows overdraft balance)");

            this._view.Write("Choose Account Type (1-2): ");

            int typeChoice = this._view.ReadChoice(1, 2);

            BankAccountType accountType = (BankAccountType)typeChoice;

            string accNumber = this._view.ReadString("Enter Account Number: ");
            decimal initBalance = this._view.ReadDecimal("Enter Initial Deposit Balance: Rs. ");

            BankAccount account = this._bankService.CreateAccount(accountType, accNumber, initBalance);

            bool finish = false;
            do
            {
                this._view.PrintDivider();
                this._view.PrintSubHeader($"Active Session: {accountType} ({account.AccountNumber})");
                this._view.ShowBankMenu();

                int bankChoice = this._view.ReadChoice(MinBankChoice, MaxBankChoice);
                this._view.Write("\n");

                switch (bankChoice)
                {
                    case 1:
                        this.Deposit(account);
                        break;

                    case 2:
                        this.Withdraw(account);
                        break;

                    case 3:
                        this._view.PrintBankAccountDetails(account);
                        break;

                    case 4:
                        finish = true;
                        break;
                }
            }
            while (!finish);
        }

        /// <summary>
        /// Process a specified amount into the given bank account.
        /// </summary>
        /// <param name="account">The bank account to deposit funds into.</param>
        public void Deposit(BankAccount account)
        {
            decimal depositAmount = this._view.ReadDecimal("Enter amount to deposit: Rs. ");
            if (this._bankService.Deposit(account, depositAmount))
            {
                this._view.PrintDepositSuccess(depositAmount);
            }

            this._view.PrintBankAccountBalance(account);
        }

        /// <summary>
        /// Processes a withdrawal transaction for the specified bank account.
        /// balance.
        /// </summary>
        /// <param name="account">The bank account from which the withdrawal is made.</param>
        public void Withdraw(BankAccount account)
        {
            decimal withdrawalAmount = this._view.ReadDecimal("Enter amount to withdraw: Rs. ");

            if (this._bankService.Withdraw(account, withdrawalAmount, out string error))
            {
                this._view.PrintWithdrawSuccess(withdrawalAmount);
            }
            else
            {
                this._view.PrintWithdrawFailure(error);
            }

            this._view.PrintBankAccountBalance(account);
        }
    }
}
