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
        public decimal GetTransactionAmountToUpdate(decimal existingAmount) => this._consoleHelper.ReadDecimal($"Enter the transaction amount [Existing: {existingAmount}] : ") ?? 0.0M;

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
        public FilterType GetFilterTypeChoice()
        {
            List<string> filterChoices = new List<string>
            {
                "Flow Type",
                "Category",
            };
            int filterIndex = this._consoleHelper.ReadSelection("Select the filtering parameter:", filterChoices);
            return (FilterType)filterIndex;
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
        public string? GetTransactionDescription() => this._consoleHelper.ReadString("Enter a description for the transaction (optional): ", isOptional: true);

        /// <inheritdoc />
        public string? GetTransactionDescriptionToUpdate(string? existingDescription) => this._consoleHelper.ReadString($"Enter a description for the transaction [Current: {existingDescription ?? "None"}] (Press Enter to skip): ", isOptional: true);

        /// <inheritdoc />
        public void DisplayTransactionsNotFound()
        {
            this._consoleHelper.WriteColored("\nNo transactions found.\n", ConsoleColor.Yellow);
        }

        /// <inheritdoc />
        public void DisplayUpdateSuccessful()
        {
            this._consoleHelper.WriteColored("\n[SUCCESS] Transaction updated successfully.\n", ConsoleColor.Green);
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
        public int GetIndexFromTable(int maxIndex)
        {
            while (true)
            {
                int choice = this._consoleHelper.ReadInt($"Enter Row Number to select (1 to {maxIndex}): ") ?? 0;

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
        public void DisplayFilteredTable(IReadOnlyList<Transaction> transactions)
        {
            this._consoleHelper.PrintSubHeader("Filtered Transactions");
            this.DisplayAsTable(transactions);
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
                .Title("[bold cyan]TRANSACTION DASHBOARD[/]")
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

        /// <summary>
        /// Displays an error message in red.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public void DisplayError(string message)
        {
            this._consoleHelper.DisplayError(message);
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

        /// <summary>
        /// Prints details of a successful stock update.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity changed.</param>
        public void PrintStockUpdation(int id, int quantity)
        {
            this._consoleHelper.WriteColored($"\n[SUCCESS] Stock adjusted successfully for Product ID = {id}.\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Prompts the user continuously using arrow keys to select a menu option.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <returns>A valid choice integer corresponding to the selection index.</returns>
        public int ReadChoice(int min, int max)
        {
            var choices = new List<string>
            {
                "1. Add a new transaction",
                "2. View all transactions",
                "3. Update an existing transaction",
                "4. Delete a transaction",
                "5. Filter transactions",
                "6. Generate Insights and Report",
                "5. Exit the application",
            };

            return this._consoleHelper.ReadSelection("Select an operation to run:", choices);
        }
    }
}
