namespace Assignment4ExpenseTracker.View
{
    using System.Linq;
    using Assignment4ExpenseTracker.IO;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Utilities;
    using Spectre.Console;

    /// <summary>
    /// Handles console-based input and output operations for the Expense Tracker application.
    /// </summary>
    internal class ConsoleView : IView
    {
        private readonly IConsoleIO _consoleIo;
        private readonly ConsoleHelper _consoleHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleIo">The IIo implementation used for console input and output.</param>
        /// <param name="consoleHelper">The console helper for generic input, output, and formatting operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when consoleIo is null.</exception>
        public ConsoleView(IConsoleIO consoleIo, ConsoleHelper consoleHelper)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._consoleHelper = consoleHelper ?? throw new ArgumentNullException(nameof(consoleHelper));
        }

        /// <inheritdoc />
        public decimal GetTransactionAmount() => this._consoleHelper.ReadPositiveDecimal("Enter the transaction amount: ") ?? 0.0M;

        /// <inheritdoc />
        public decimal GetTransactionAmountToUpdate(decimal existingAmount) => this._consoleHelper.ReadPositiveDecimal($"Enter the transaction amount [Existing: {existingAmount}] (Press Enter to keep current): ", isOptional: true) ?? existingAmount;

        /// <inheritdoc />
        public TransactionType GetTransactionTypeChoice(TransactionType? existingType = null)
        {
            List<string> typeChoices = new List<string>
            {
                "Income",
                "Expense",
            };
            if (existingType.HasValue)
            {
                typeChoices.Insert(0, $"Keep current ({existingType.Value})");
            }

            int index = this._consoleHelper.ReadSelection("Select the cash flow type:", typeChoices);
            if (existingType.HasValue)
            {
                if (index == 1)
                {
                    return existingType.Value;
                }

                return (TransactionType)(index - 1);
            }

            return (TransactionType)index;
        }

        /// <inheritdoc />
        public PaymentMethod GetPaymentMethod(PaymentMethod? existingMethod = null)
        {
            List<string> paymentChoices = new List<string>
            {
                "Cash",
                "Credit Card",
                "Debit Card",
                "Bank Transfer",
            };
            if (existingMethod.HasValue)
            {
                paymentChoices.Insert(0, $"Keep current ({existingMethod.Value})");
            }

            int paymentIndex = this._consoleHelper.ReadSelection("Select the payment method:", paymentChoices);
            if (existingMethod.HasValue)
            {
                if (paymentIndex == 1)
                {
                    return existingMethod.Value;
                }

                return (PaymentMethod)(paymentIndex - 1);
            }

            return (PaymentMethod)paymentIndex;
        }

        /// <inheritdoc />
        public TransactionCategory GetIncomeCategory(TransactionCategory? existingCategory = null)
        {
            IReadOnlyList<TransactionCategory> incomeCategories = new List<TransactionCategory>()
            {
                TransactionCategory.Salary,
                TransactionCategory.Investment,
                TransactionCategory.MiscellaneousIncome,
            };
            List<string> choices = incomeCategories.Select(c => c.ToString()).ToList();
            if (existingCategory.HasValue)
            {
                choices.Insert(0, $"Keep current ({existingCategory.Value})");
            }

            int categoryIndex = this._consoleHelper.ReadSelection("Select the income category:", choices);
            if (existingCategory.HasValue)
            {
                if (categoryIndex == 1)
                {
                    return existingCategory.Value;
                }

                return incomeCategories[categoryIndex - 2];
            }

            return incomeCategories[categoryIndex - 1];
        }

        /// <inheritdoc />
        public TransactionCategory GetExpenseCategory(TransactionCategory? existingCategory = null)
        {
            IReadOnlyList<TransactionCategory> expenseCategories = new List<TransactionCategory>()
            {
                TransactionCategory.Transport,
                TransactionCategory.Utilities,
                TransactionCategory.Groceries,
                TransactionCategory.Rent,
                TransactionCategory.Food,
                TransactionCategory.Shopping,
                TransactionCategory.MiscellaneousExpense,
            };
            List<string> choices = expenseCategories.Select(c => c.ToString()).ToList();
            if (existingCategory.HasValue)
            {
                choices.Insert(0, $"Keep current ({existingCategory.Value})");
            }

            int categoryIndex = this._consoleHelper.ReadSelection("Select the expense category:", choices);
            if (existingCategory.HasValue)
            {
                if (categoryIndex == 1)
                {
                    return existingCategory.Value;
                }

                return expenseCategories[categoryIndex - 2];
            }

            return expenseCategories[categoryIndex - 1];
        }

        /// <inheritdoc />
        public string? GetTransactionDescription() => this._consoleHelper.ReadString("Enter a description for the transaction (optional): ", isOptional: true);

        /// <inheritdoc />
        public string? GetTransactionDescriptionToUpdate(string? existingDescription) => this._consoleHelper.ReadString($"Enter a description for the transaction [Current: {existingDescription ?? "None"}] (Press Enter to keep current): ", isOptional: true) ?? existingDescription;

        /// <inheritdoc />
        public void DisplayTransactionsNotFound()
        {
            this._consoleHelper.WriteColored("\nNo transactions found.\n", ConsoleColor.Yellow);
        }

        /// <inheritdoc />
        public void DisplayUpdateSuccessful()
        {
            this._consoleHelper.DisplaySuccessMessage("Transaction Updated successfully.");
        }

        /// <inheritdoc />
        public void DisplayAddSuccessful()
        {
            this._consoleHelper.DisplaySuccessMessage("Transaction Added Successfully.");
        }

        /// <inheritdoc />
        public void DisplayDeleteSuccessful()
        {
            this._consoleHelper.DisplaySuccessMessage("Transaction Deleted Successfully.");
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

        /// <inheritdoc />
        public void DisplayDeleteHeader()
        {
            this._consoleHelper.PrintHeader("DELETE TRANSACTION");
        }

        /// <inheritdoc />
        public void DisplayAddHeader()
        {
            this._consoleHelper.PrintHeader("ADD NEW TRANSACTION");
        }

        /// <inheritdoc />
        public void DisplayUpdateHeader()
        {
            this._consoleHelper.PrintHeader("UPDATE TRANSACTION");
        }

        /// <inheritdoc />
        public void DisplayReportHeader()
        {
            this._consoleHelper.PrintHeader("FINANCIAL INSIGHTS & REPORT");
        }

        /// <inheritdoc />
        public void DisplayAllTransactionsHeader()
        {
            this._consoleHelper.PrintHeader("TRANSACTION DASHBOARD");
        }

        /// <inheritdoc />
        public int GetIndexFromTable(int maxIndex)
        {
            while (true)
            {
                int choice = this._consoleHelper.ReadPositiveInt($"Enter Row Number to select (1 to {maxIndex}): ") ?? 0;

                if (choice >= 1 && choice <= maxIndex)
                {
                    return choice - 1;
                }

                this._consoleHelper.DisplayError($"Invalid selection. Please choose a row between 1 and {maxIndex}.\n");
            }
        }

        /// <summary>
        /// Displays the details of a given transaction.
        /// </summary>
        /// <param name="transaction">The transaction object containing the details to be displayed.</param>
        /// <inheritdoc />
        public void DisplayTransactionDetails(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            this._consoleIo.WriteLine("Transaction Details:");
            this._consoleIo.WriteLine($"ID: {transaction.Id}");
            this._consoleIo.WriteLine($"Amount: {transaction.Amount:C}");
            this._consoleIo.WriteLine($"Date: {transaction.TimeStamp:yyyy-MM-dd HH:mm}");
            this._consoleIo.WriteLine($"Transaction Type: {transaction.Type}");
            this._consoleIo.WriteLine($"Payment Method: {transaction.Method}");
            this._consoleIo.WriteLine($"Category: {transaction.Category}");
            this._consoleIo.WriteLine($"Description: {Markup.Escape(transaction.Description ?? "N/A")}");
        }

        /// <inheritdoc />
        public void DisplayAsTable(IReadOnlyList<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No transactions recorded yet.[/]\n");
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[bold cyan]ALL TRANSACTIONS [/]")
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

                if (transaction.Type == TransactionType.Income)
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

        /// <summary>
        /// Displays an error message in red.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public void HandleError(string message)
        {
            this._consoleHelper.DisplayError(message);
            this.PauseAndReturn();
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void ClearScreen()
        {
            this._consoleHelper.ClearScreen();
        }

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        public void PauseAndReturn()
        {
            this._consoleHelper.PauseAndReturn();
        }

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        public void PrintGoodbye()
        {
            this._consoleHelper.PrintGoodbye();
        }

        /// <inheritdoc />
        public void DisplayInsights(decimal totalIncome, decimal totalExpense, decimal netBalance, int totalTransactions)
        {
            this._consoleHelper.PrintSubHeader("FINANCIAL INSIGHTS & REPORT");

            var panel = new Panel(new Markup(
                $"[bold]Total Transactions:[/] {totalTransactions}\n" +
                $"[bold green]Total Income:[/] {totalIncome:C}\n" +
                $"[bold red]Total Expenses:[/] {totalExpense:C}\n" +
                $"[bold]Net Balance:[/] {(netBalance >= 0 ? $"[green]+{netBalance:C}[/]" : $"[red]{netBalance:C}[/]")}"))
            {
                Header = new PanelHeader("[bold yellow]Report Summary[/]"),
                Border = BoxBorder.Rounded,
                Padding = new Padding(2, 1, 2, 1),
            };

            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Prompts the user continuously using arrow keys to select a menu option.
        /// </summary>
        /// <returns>A valid choice integer corresponding to the selection index.</returns>
        public int ReadChoice()
        {
            var choices = new List<string>
            {
                "1. Add a new transaction",
                "2. View all transactions",
                "3. Update an existing transaction",
                "4. Delete a transaction",
                "5. Generate Insights and Report",
                "6. Exit the application",
            };

            return this._consoleHelper.ReadSelection("Select an operation to run:", choices);
        }

        /// <inheritdoc />
        public void ShowMainMenu()
        {
            this._consoleHelper.PrintHeader("EXPENSE TRACKER SYSTEM");

            var table = new Table()
                .Title("[bold yellow]Available Operations[/]");

            table.AddColumn(new TableColumn("[bold yellow]Option[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Operation[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold yellow]Description[/]").LeftAligned());

            table.AddRow("1", "Add Transaction", "Adds a new income or expense transaction with details.");
            table.AddRow("2", "View Transactions", "Displays all recorded transactions in a dashboard.");
            table.AddRow("3", "Update Transaction", "Modifies the details of an existing transaction.");
            table.AddRow("4", "Delete Transaction", "Permanently removes a transaction record.");
            table.AddRow("5", "Generate Report", "Displays financial insights and net balance summary.");
            table.AddRow("6", "Exit", "Exits the Application.");

            AnsiConsole.Write(table);
            this._consoleHelper.WriteLine(string.Empty);
        }
    }
}
