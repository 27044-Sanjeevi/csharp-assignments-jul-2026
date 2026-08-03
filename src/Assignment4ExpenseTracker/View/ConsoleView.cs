namespace Assignment4ExpenseTracker.View
{
    using Assignment4ExpenseTracker.IO;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Services.Validation;
    using Assignment4ExpenseTracker.Utilities;
    using Spectre.Console;

    /// <summary>
    /// Handles console-based input and output operations for the Expense Tracker application.
    /// </summary>
    internal class ConsoleView : IView
    {
        private const int OffsetForExpenseList = 3; // Offset to align expense categories with the enum values
        private readonly IIo _consoleIo;
        private readonly ConsoleHelper _consoleHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleIo">The IIo implementation used for console input and output.</param>
        /// <param name="consoleHelper">The console helper for generic input, output, and formatting operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when consoleIo is null.</exception>
        public ConsoleView(IIo consoleIo, ConsoleHelper consoleHelper)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._consoleHelper = consoleHelper ?? throw new ArgumentNullException(nameof(consoleHelper));
        }

        /// <inheritdoc />
        public void DisplayMenu()
        {
            Console.WriteLine("Welcome to the Expense Tracker!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Transactions");
            Console.WriteLine("3. Exit");
        }

        /// <inheritdoc />
        public decimal GetTransactionAmount() => this._consoleHelper.ReadDecimal("Enter the transaction amount: ") ?? 0.0M;

        /// <inheritdoc />
        public decimal GetTransactionAmountToUpdate(decimal existingAmount) => this._consoleHelper.ReadDecimal($"Enter the transaction amount: [Existing: {existingAmount}") ?? 0.0M;

        /// <inheritdoc />
        public FlowType GetFlowChoice()
        {
            List<string> flowChoices = new List<string>
            {
                "Income",
                "Expense",
            };
            int flowIndex = this._consoleHelper.ReadSelection("Select the flow type:", flowChoices);
            return (FlowType)flowIndex;
        }

        /// <inheritdoc />
        public PaymentMethod GetPaymentMethod()
        {
            List<string> paymentChoices = new List<string>
            {
                "Cash",
                "Credit Card",
                "Debit Card",
                "Bank Transfer",
            };
            int paymentIndex = this._consoleHelper.ReadSelection("Select the payment method:", paymentChoices);
            return (PaymentMethod)paymentIndex;
        }

        /// <inheritdoc />
        public TransactionCategory GetIncomeCategory()
        {
            List<string> incomeCategories = new List<string>
            {
                "Salary",
                "Investments",
                "Others",
            };
            int categoryIndex = this._consoleHelper.ReadSelection("Select the income category:", incomeCategories);
            return (TransactionCategory)categoryIndex;
        }

        /// <inheritdoc />
        public TransactionCategory GetExpenseCategory()
        {
            List<string> expenseCategories = new List<string>
            {
                "Transport",
                "Utilities",
                "Groceries",
                "Rent",
                "Food",
                "Shopping",
                "Others",
            };
            int categoryIndex = this._consoleHelper.ReadSelection("Select the expense category:", expenseCategories);
            return (TransactionCategory)(categoryIndex + OffsetForExpenseList);
        }

        /// <inheritdoc />
        public string? GetTransactionDescription() => this._consoleHelper.ReadString("Enter a description for the transaction (optional): ");

        /// <inheritdoc />
        public string? GetTransactionDescriptionToUpdate(string? existingDescription) => this._consoleHelper.ReadString($"Enter a description for the transaction [Current: {existingDescription ?? "None"}] (Press Enter to skip): ", isOptional: true);

        /// <inheritdoc />
        public Transaction GetTransactionDetailsToUpdate(Transaction existing)
        {
            ArgumentNullException.ThrowIfNull(existing, nameof(existing));

            this._consoleHelper.PrintHeader($"Editing Transaction: {existing.Id}");

            decimal amount = this._consoleHelper.ReadDecimal($"Enter the transaction amount [Current: Rs. {existing.Amount}] (Press Enter to skip): ", isOptional: true) ?? existing.Amount;

            FlowType flowType = this.GetFlowChoice();
            PaymentMethod paymentMethod = this.GetPaymentMethod();
            TransactionCategory category;
            category = flowType == FlowType.Income
                    ? this.GetIncomeCategory()
                    : this.GetExpenseCategory();

            string? inputDescription = this._consoleHelper.ReadString($"Enter a description [Current: {existing.Description ?? "None"}] (Press Enter to skip): ", isOptional: true);

            string? description = string.IsNullOrWhiteSpace(inputDescription) ? existing.Description : inputDescription;

            return new Transaction(existing.Id)
            {
                Amount = amount,
                TimeStamp = existing.TimeStamp,
                Category = category,
                Type = flowType,
                Method = paymentMethod,
                Description = description,
            };
        }

        /// <inheritdoc />
        public void DisplayValidationResult(Services.Validation.ValidationResult result)
        {
            ArgumentNullException.ThrowIfNull(result, nameof(result));
            if (!result.IsValid)
            {
                this._consoleHelper.DisplayError("\n Validation Failed: \n");
                foreach (string error in result.Errors)
                {
                    this._consoleHelper.WriteColored($"  - {error}\n", ConsoleColor.Red);
                }

                this._consoleHelper.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Displays the details of a given transaction.
        /// </summary>
        /// <param name="transaction">The transaction object containing the details to be displayed.</param>
        public void DisplayTransactionDetails(Transaction transaction)
        {
            Console.WriteLine("Transaction Details:");
            Console.WriteLine($"ID: {transaction.Id}");
            Console.WriteLine($"Amount: {transaction.Amount}");
            Console.WriteLine($"Date: {transaction.TimeStamp}");
            Console.WriteLine($"Flow Type: {transaction.Type}");
            Console.WriteLine($"Payment Method: {transaction.Method}");
            Console.WriteLine($"Source Category: {transaction.Category}");
        }

        /// <inheritdoc />
        public void DisplayAsTable(List<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No transactions recorded yet.[/]\n");
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[bold cyan]TRANSACTION LEDGER DASHBOARD[/]")
                .Caption($"Total Records: {transactions.Count}");

            table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Date & Time[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Type[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Category[/]"));
            table.AddColumn(new TableColumn("[bold]Method[/]"));
            table.AddColumn(new TableColumn("[bold]Amount[/]").RightAligned());
            table.AddColumn(new TableColumn("[bold]Description[/]"));

            int index = 1;

            foreach (Transaction transaction in transactions)
            {
                if (transaction == null)
                {
                    continue;
                }

                string displayId = index.ToString();
                string formattedDate = transaction.TimeStamp.ToString("yyyy-MM-dd HH:mm");
                string amountDisplay;
                string typeDisplay;

                if (transaction.Type == FlowType.Income)
                {
                    typeDisplay = "[green]INCOME[/]";
                    amountDisplay = $"[green]+{transaction.Amount:C}[/]";
                }
                else
                {
                    typeDisplay = "[red]EXPENSE[/]";
                    amountDisplay = $"[red]-{transaction.Amount:C}[/]";
                }

                string descriptionDisplay = string.IsNullOrWhiteSpace(transaction.Description) ? "[grey]N/A[/]" : Markup.Escape(transaction.Description);

                table.AddRow(
                    displayId,
                    formattedDate,
                    typeDisplay,
                    transaction.Category.ToString(),
                    transaction.Method.ToString(),
                    amountDisplay,
                    descriptionDisplay);

                index++;
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
    }
}
