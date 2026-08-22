namespace Assignment4ExpenseTracker.View
{
    using System.Dynamic;
    using Assignment4ExpenseTracker.IO;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Utilities;
    using Assignment4ExpenseTracker.View.Interfaces;
    using Spectre.Console;

    /// <summary>
    /// Handles console-based input and output operations for the Expense Tracker application.
    /// </summary>
    internal class ConsoleView : IView
    {
        private readonly TransactionType[] _transactionType =
        {
            TransactionType.Income,
            TransactionType.Expense,
        };

        private readonly PaymentMethod[] _paymentMethod =
        {
             PaymentMethod.Cash,
             PaymentMethod.CreditCard,
             PaymentMethod.DebitCard,
             PaymentMethod.BankTransfer,
        };

        private readonly TransactionCategory[] _incomeCategory =
        {
             TransactionCategory.Salary,
             TransactionCategory.Investment,
             TransactionCategory.Freelance,
             TransactionCategory.Business,
             TransactionCategory.Gifts,
             TransactionCategory.MiscellaneousIncome,
        };

        private readonly TransactionCategory[] _expenseCategory =
        {
            TransactionCategory.Transport,
            TransactionCategory.Utilities,
            TransactionCategory.Groceries,
            TransactionCategory.Rent,
            TransactionCategory.Food,
            TransactionCategory.Shopping,
            TransactionCategory.Healthcare,
            TransactionCategory.Education,
            TransactionCategory.MiscellaneousExpense,
        };

        private readonly FilterType[] _filterType =
        {
            FilterType.TransactionType,
            FilterType.Category,
        };

        private readonly SortBy[] _sortBy =
        {
            SortBy.Date,
            SortBy.Amount,
            SortBy.Category,
        };

        private readonly SortOrder[] _sortOrder =
        {
            SortOrder.Ascending,
            SortOrder.Descending,
        };

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
            return this.GetEnumSelection("Select the cash flow type:", this._transactionType, existingType);
        }

        /// <inheritdoc />
        public FilterType GetFilterTypeChoice(FilterType? existingType = null)
        {
            return this.GetEnumSelection("Select the filtering parameter:", this._filterType, existingType);
        }

        /// <inheritdoc />
        public PaymentMethod GetPaymentMethod(PaymentMethod? existingMethod = null)
        {
            return this.GetEnumSelection("Select the payment method:", this._paymentMethod, existingMethod);
        }

        /// <inheritdoc />
        public TransactionCategory GetIncomeCategory(TransactionCategory? existingCategory = null)
        {
            return this.GetEnumSelection("Select the income category:", this._incomeCategory, existingCategory);
        }

        /// <inheritdoc />
        public TransactionCategory GetExpenseCategory(TransactionCategory? existingCategory = null)
        {
            return this.GetEnumSelection("Select the expense category:", this._expenseCategory, existingCategory);
        }

        /// <inheritdoc />
        public string? GetTransactionDescription() => this._consoleHelper.ReadString("Enter a description for the transaction (optional): ", isOptional: true);

        /// <inheritdoc />
        public string? GetTransactionDescriptionToUpdate(string? existingDescription) => this._consoleHelper.ReadString($"Enter a description for the transaction [Current: {existingDescription ?? "None"}] (Press Enter to keep current): ", isOptional: true) ?? existingDescription;

        /// <inheritdoc />
        public void DisplayTransactionsNotFound() => this._consoleHelper.WriteColored("\nNo transactions found.\n", ConsoleColor.Yellow);

        /// <inheritdoc />
        public void DisplayAddSuccessful() => this._consoleHelper.DisplaySuccessMessage("Transaction Added Successfully.");

        /// <inheritdoc />
        public void DisplayUpdateSuccessful() => this._consoleHelper.DisplaySuccessMessage("Transaction Updated successfully.");

        /// <inheritdoc />
        public void DisplayDeleteSuccessful() => this._consoleHelper.DisplaySuccessMessage("Transaction Deleted Successfully.");

        /// <inheritdoc />
        public bool ConfirmDelete(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));

            return this.ReadSelection(
                $"Are you sure you want to permanently delete this {transaction.Type} transaction of {transaction.Amount:C}?",
                new[] { false, true },
                confirm => confirm ? "Yes, permanently Delete" : "No, Cancel deletion");
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
        public DateTime? GetDateTime(DateTime? existingDateTime, bool isOptional)
        {
            return this._consoleHelper.ReadDateTime("\nEnter transaction date (YYYY-MM-DD) or press Enter for current date: ", existingDateTime: existingDateTime, isOptional: isOptional);
        }

        /// <inheritdoc />
        public void DisplayDeleteHeader() => this._consoleHelper.PrintHeader("DELETE TRANSACTION");

        /// <inheritdoc />
        public void DisplayAddHeader() => this._consoleHelper.PrintHeader("ADD NEW TRANSACTION");

        /// <inheritdoc />
        public void DisplayUpdateHeader() => this._consoleHelper.PrintHeader("UPDATE TRANSACTION");

        /// <inheritdoc />
        public void DisplaySearchHeader() => this._consoleHelper.PrintHeader("SEARCH ACROSS TRANSACTIONS");

        /// <inheritdoc />
        public void DisplayReportHeader() => this._consoleHelper.PrintHeader("FINANCIAL INSIGHTS & REPORT");

        /// <inheritdoc />
        public void DisplayAllTransactionsHeader() => this._consoleHelper.PrintHeader("TRANSACTION DASHBOARD");

        /// <inheritdoc />
        public void DisplayFilterHeader() => this._consoleHelper.PrintHeader("FILTER TRANSACTIONS");

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

        /// <inheritdoc />
        public string GetSearchKeyword() => this._consoleHelper.ReadString("Enter the keyword to search across all your transactions : ")?.Trim().ToLower() ?? string.Empty;

        /// <inheritdoc />
        public (SortBy sortBy, SortOrder order) GetSortingCriteria()
        {
            this._consoleHelper.PrintHeader("SORT TRANSACTIONS");
            SortBy sortBy = this.GetEnumSelection("Select field to sort by:", this._sortBy, SortBy.Date);
            SortOrder order = this.GetEnumSelection("Select sort order:", this._sortOrder, SortOrder.Ascending);
            return (sortBy, order);
        }

        /// <inheritdoc />
        public void DisplayTransactionDetails(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            this._consoleIo.WriteLine("Transaction Details:");
            this._consoleIo.WriteLine($"ID: {transaction.Id}");
            this._consoleIo.WriteLine($"Amount: {transaction.Amount:C}");
            this._consoleIo.WriteLine($"Date: {transaction.Timestamp:yyyy-MM-dd HH:mm}");
            this._consoleIo.WriteLine($"Transaction Type: {transaction.Type}");
            this._consoleIo.WriteLine($"Payment Method: {transaction.Method}");
            this._consoleIo.WriteLine($"Category: {transaction.Category}");
            this._consoleIo.WriteLine($"Description: {Markup.Escape(transaction.Description ?? "N/A")}");
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
                string formattedDate = transaction.Timestamp.ToString("yyyy-MM-dd HH:mm");
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

        /// <inheritdoc />
        public void DisplayVisualCharts(IReadOnlyList<Transaction> transactions)
        {
            var validTransactions = transactions?
                .Where(t => t != null)
                .ToList() ?? new List<Transaction>();

            if (validTransactions.Count == 0)
            {
                return;
            }

            decimal totalIncome = validTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => Math.Abs(t.Amount));

            decimal totalExpense = validTransactions
                .Where(t => t.Type != TransactionType.Income)
                .Sum(t => Math.Abs(t.Amount));

            var expenseByCategory = validTransactions
                .Where(t => t.Type != TransactionType.Income)
                .GroupBy(t => t.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(t => Math.Abs(t.Amount)));

            // 1. Cash Flow Breakdown Chart
            AnsiConsole.MarkupLine("[bold yellow]Cash Flow Breakdown[/]");
            var flowChart = new BreakdownChart().Width(60);

            if (totalIncome > 0)
            {
                flowChart.AddItem("Income", (double)totalIncome, Color.Green);
            }

            if (totalExpense > 0)
            {
                flowChart.AddItem("Expense", (double)totalExpense, Color.Red);
            }

            if (totalIncome > 0 || totalExpense > 0)
            {
                AnsiConsole.Write(flowChart);
                AnsiConsole.WriteLine();
            }
            else
            {
                AnsiConsole.MarkupLine("[grey]No data available for Cash Flow breakdown.[/]\n");
            }

            // 2. Expenses by Category Chart
            AnsiConsole.MarkupLine("[bold yellow]Expenses by Category[/]");
            if (expenseByCategory.Count > 0)
            {
                var categoryChart = new BarChart()
                    .Width(60)
                    .Label("[red]Category Expenses (Amount)[/]");

                foreach (var pair in expenseByCategory.OrderByDescending(p => p.Value))
                {
                    Color color = this.GetCategoryColor(pair.Key);
                    categoryChart.AddItem(pair.Key.ToString(), (double)pair.Value, color);
                }

                AnsiConsole.Write(categoryChart);
                AnsiConsole.WriteLine();
            }
            else
            {
                AnsiConsole.MarkupLine("[grey]No expenses recorded to display category breakdown.[/]\n");
            }
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
            this._consoleHelper.DisplayExitMessage();
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
        /// <returns>An enum specifying the main menu option.</returns>
        public MainMenuOption ReadChoice()
        {
            return this.ReadSelection(
                "Select an operation to run:",
                Enum.GetValues<MainMenuOption>(),
                option => option switch
                {
                    MainMenuOption.Add => "1. Add a new transaction",
                    MainMenuOption.ViewAll => "2. View all transactions",
                    MainMenuOption.Update => "3. Update an existing transaction",
                    MainMenuOption.Delete => "4. Delete a transaction",
                    MainMenuOption.Filter => "5. Filter transactions",
                    MainMenuOption.Sort => "6. Sort transactions",
                    MainMenuOption.Search => "7. Search across transactions",
                    MainMenuOption.GenerateReport => "8. Generate Insights and Report",
                    MainMenuOption.Exit => "9. Exit the application",
                    _ => option.ToString()
                });
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
            table.AddRow("5", "Filter Transactions", "Filters transactions by transaction type or category.");
            table.AddRow("6", "Sort Transactions", "Sort transactions by amount, date or category.");
            table.AddRow("7", "Search Transactions", "Search by any fields of the transaction.");
            table.AddRow("8", "Display Report", "Displays financial insights and net balance summary.");
            table.AddRow("9", "Exit", "Exits the Application.");

            AnsiConsole.Write(table);
            this._consoleHelper.WriteLine(string.Empty);
        }

        /// <inheritdoc />
        public T ReadSelection<T>(string title, IEnumerable<T> choices, Func<T, string>? displaySelector = null)
        {
            ArgumentNullException.ThrowIfNull(choices);
            Func<T, string> toDisplay = displaySelector ?? (item => item?.ToString() ?? string.Empty);

            List<T> choiceList = choices.ToList();
            List<string> displayChoices = choiceList.Select(toDisplay).ToList();

            var prompt = new SelectionPrompt<string>()
                .Title(title)
                .HighlightStyle(new Style(Color.Black, Color.Aqua))
                .AddChoices(displayChoices);

            string selected = AnsiConsole.Prompt(prompt);

            int index = displayChoices.IndexOf(selected);
            return choiceList[index];
        }

        /// <summary>
        /// Maps an Expense/Income category to a unique color for charts.
        /// </summary>
        private Color GetCategoryColor(TransactionCategory category)
        {
            return category switch
            {
                TransactionCategory.Salary => Color.Green,
                TransactionCategory.Investment => Color.Teal,
                TransactionCategory.Freelance => Color.LightGreen,
                TransactionCategory.Business => Color.DarkGreen,
                TransactionCategory.Gifts => Color.Pink1,
                TransactionCategory.MiscellaneousIncome => Color.GreenYellow,
                TransactionCategory.Transport => Color.SkyBlue1,
                TransactionCategory.Utilities => Color.Purple,
                TransactionCategory.Groceries => Color.DarkOrange,
                TransactionCategory.Rent => Color.Red,
                TransactionCategory.Food => Color.Yellow,
                TransactionCategory.Shopping => Color.Purple3,
                TransactionCategory.Healthcare => Color.Red3,
                TransactionCategory.Education => Color.Blue,
                TransactionCategory.MiscellaneousExpense => Color.Grey,
                _ => Color.White
            };
        }

        /// <summary>
        /// Prompts the user to select an enum value, optionally allowing them to keep the current value.
        /// </summary>
        private T GetEnumSelection<T>(string title, IReadOnlyList<T> choices, T? existingValue = null)
            where T : struct, Enum
        {
            if (existingValue == null)
            {
                return this.ReadSelection(title, choices);
            }

            List<T?> options = new List<T?> { null };
            options.AddRange(choices.Cast<T?>());

            T? selected = this.ReadSelection(
                title,
                options,
                option => option == null ? $"Keep current ({existingValue.Value})" : option.Value.ToString());

            return selected ?? existingValue.Value;
        }
    }
}
