namespace Assignment2BasicsOfOOPs.Controller
{
    using Assignment2BasicsOfOOPs.Models;
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
            this._view = view;
            this._bankService = bankService;
        }

        /// <summary>
        /// Prompts the user and runs Task 3 (Bank System) interactively.
        /// </summary>
        public void RunBankTask()
        {
            this._view.PrintHeader("Task 3: Bank System");

            // 1. Create Bank Account
            this._view.PrintSubHeader("Account Setup");
            Console.WriteLine("1. Savings Account (Requires positive balance, overdraft not allowed)");
            Console.WriteLine("2. Checking Account (Allows overdraft balance)");
            double typeChoice = this._view.ReadDouble("Choose Account Type (1-2): ");
            while (typeChoice != 1.0 && typeChoice != 2.0)
            {
                typeChoice = this._view.ReadDouble("[INPUT ERROR] Enter 1 or 2 only: ");
            }

            string accNumber = this._view.ReadString("Enter Account Number: ");
            decimal initBalance = this._view.ReadDecimal("Enter Initial Deposit Balance: Rs. ");

            BankAccount account;
            if (typeChoice == 1.0)
            {
                account = new SavingsAccount(accNumber, initBalance);
            }
            else
            {
                account = new CheckingAccount(accNumber, initBalance);
            }

            // 2. Bank Action Loop
            bool finish = false;
            do
            {
                this._view.PrintDivider();
                string accTypeTitle = account is SavingsAccount ? "Savings Account" : "Checking Account";
                this._view.PrintSubHeader($"Active Session: {accTypeTitle} ({account.AccountNumber})");
                this._view.ShowBankMenu();
                int bankChoice = this._view.ReadBankChoice(MinBankChoice, MaxBankChoice);
                Console.WriteLine();

                switch (bankChoice)
                {
                    case 1:
                        decimal depAmount = this._view.ReadDecimal("Enter amount to deposit: Rs. ");
                        if (this._bankService.Deposit(account, depAmount))
                        {
                            this._view.PrintDepositSuccess(depAmount);
                        }

                        this._view.PrintBankAccountBalance(account);
                        break;

                    case 2:
                        decimal withAmount = this._view.ReadDecimal("Enter amount to withdraw: Rs. ");
                        this._view.PrintWithdrawAttempt(withAmount);
                        if (this._bankService.Withdraw(account, withAmount, out string error))
                        {
                            this._view.PrintWithdrawSuccess(withAmount);
                        }
                        else
                        {
                            this._view.PrintWithdrawFailure(error);
                        }

                        this._view.PrintBankAccountBalance(account);
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
    }
}
