namespace Assignment5ExpenseTrackerEnhanced.View
{
    using System.Linq;
    using Assignment5ExpenseTrackerEnhanced.IO;
    using Assignment5ExpenseTrackerEnhanced.Models;
    using Assignment5ExpenseTrackerEnhanced.Models.Enums;
    using Assignment5ExpenseTrackerEnhanced.Utilities;
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
        public void DisplayFilterHeader()
        {
            this._consoleHelper.PrintHeader("FILTER TRANSACTIONS");
        }

        /// <inheritdoc />
        public FilterType GetFilterTypeChoice()
        {
            List<string> filterChoices = new List<string>
            {
                "Transaction Type",
                "Category",
            };
            int filterIndex = this._consoleHelper.ReadSelection("Select the filtering parameter:", filterChoices);
            return (FilterType)filterIndex;
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
                "5. Search transactions",
                "6. Sort transactions",
                "7. Generate Insights and Report",
                "8. Exit the application",
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
            table.AddRow("5", "Search Transactions", "Filters and displays transactions matching search filters.");
            table.AddRow("6", "Sort Transactions", "Displays transactions sorted by dynamic criteria.");
            table.AddRow("7", "Generate Report", "Displays financial insights, net balance, and charts.");
            table.AddRow("8", "Exit", "Exits the Application.");

            AnsiConsole.Write(table);
            this._consoleHelper.WriteLine(string.Empty);
        }

        /// <inheritdoc />
        public (TransactionType? type, TransactionCategory? category, PaymentMethod? method, string? keyword, bool isCancelled) GetSearchCriteria()
        {
            this._consoleHelper.PrintHeader("SEARCH TRANSACTIONS");

            TransactionType? type = null;
            TransactionCategory? category = null;
            PaymentMethod? method = null;
            string? keyword = null;

            bool searchReady = false;
            while (!searchReady)
            {
                var filterOptions = new List<string>
                {
                    $"[1] Flow Type: {(type.HasValue ? $"[green]{type.Value}[/]" : "[grey]Any[/]")}",
                    $"[2] Category: {(category.HasValue ? $"[green]{category.Value}[/]" : "[grey]Any[/]")}",
                    $"[3] Payment Method: {(method.HasValue ? $"[green]{method.Value}[/]" : "[grey]Any[/]")}",
                    $"[4] Description Keyword: {(!string.IsNullOrEmpty(keyword) ? $"[green]\"{keyword}\"[/]" : "[grey]Any[/]")}",
                    "[bold yellow]=> Run Search Now[/]",
                    "[bold red]=> Cancel Search[/]",
                };

                int selection = this._consoleHelper.ReadSelection("Select search filters to apply:", filterOptions);
                switch (selection)
                {
                    case 1:
                        type = this.GetOptionalTransactionType();
                        if (type.HasValue && category.HasValue)
                        {
                            if (!this.IsValidCategoryCombination(type.Value, category.Value))
                            {
                                category = null;
                            }
                        }

                        break;
                    case 2:
                        category = this.GetOptionalCategory(type);
                        break;
                    case 3:
                        method = this.GetOptionalPaymentMethod();
                        break;
                    case 4:
                        keyword = this._consoleHelper.ReadString("Enter description keyword (optional): ", isOptional: true);
                        break;
                    case 5:
                        searchReady = true;
                        break;
                    case 6:
                        return (null, null, null, null, true); // Cancelled
                }
            }

            return (type, category, method, keyword, false);
        }

        /// <inheritdoc />
        public TransactionType GetTransactionType(TransactionType? existingType = null)
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

            int index = this._consoleHelper.ReadSelection("Select the transaction type:", typeChoices);
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
        public void DisplayFilteredTable(IReadOnlyList<Transaction> transactions)
        {
            this._consoleHelper.PrintSubHeader("Filtered Transactions");
            this.DisplayAsTable(transactions);
        }
        /// <inheritdoc />
        public (SortBy sortBy, bool ascending) GetSortingCriteria()
        {
            this._consoleHelper.PrintHeader("SORT TRANSACTIONS");

            var fieldChoices = new List<string>
            {
                "Date & Time",
                "Amount",
                "Category",
            };

            int fieldIndex = this._consoleHelper.ReadSelection("Select field to sort by:", fieldChoices);
            SortBy sortBy = fieldIndex switch
            {
                1 => SortBy.Date,
                2 => SortBy.Amount,
                3 => SortBy.Category,
                _ => SortBy.Date,
            };

            var orderChoices = new List<string>
            {
                "Ascending",
                "Descending",
            };

            int orderIndex = this._consoleHelper.ReadSelection("Select sort order:", orderChoices);
            bool ascending = orderIndex == 1;

            return (sortBy, ascending);
        }

        /// <inheritdoc />
        public void DisplayVisualCharts(IReadOnlyList<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
            {
                return;
            }

            decimal totalIncome = 0;
            decimal totalExpense = 0;
            Dictionary<TransactionCategory, decimal> expenseByCategory = new Dictionary<TransactionCategory, decimal>();

            foreach (Transaction transaction in transactions)
            {
                if (transaction == null)
                {
                    continue;
                }

                if (transaction.Type == TransactionType.Income)
                {
                    totalIncome += transaction.Amount;
                }
                else
                {
                    totalExpense += transaction.Amount;
                    if (expenseByCategory.ContainsKey(transaction.Category))
                    {
                        expenseByCategory[transaction.Category] += transaction.Amount;
                    }
                    else
                    {
                        expenseByCategory[transaction.Category] = transaction.Amount;
                    }
                }
            }

            // 1. Cash Flow Breakdown Chart (Income vs Expense Ratio)
            AnsiConsole.MarkupLine("[bold yellow]Cash Flow Breakdown[/]");
            var flowChart = new BreakdownChart()
                .Width(60);

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

                foreach (KeyValuePair<TransactionCategory, decimal> pair in expenseByCategory.OrderByDescending(p => p.Value))
                {
                    categoryChart.AddItem(pair.Key.ToString(), (double)pair.Value, this.GetCategoryColor(pair.Key));
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
        /// Selects an optional transaction flow type.
        /// </summary>
        /// <returns>The chosen TransactionType, or null for Any.</returns>
        private TransactionType? GetOptionalTransactionType()
        {
            var choices = new List<string> { "Any", "Income", "Expense" };
            int selection = this._consoleHelper.ReadSelection("Select flow type:", choices);
            return selection == 1 ? null : (TransactionType)(selection - 2);
        }

        /// <summary>
        /// Selects an optional payment method.
        /// </summary>
        /// <returns>The chosen PaymentMethod, or null for Any.</returns>
        private PaymentMethod? GetOptionalPaymentMethod()
        {
            var choices = new List<string> { "Any", "Cash", "Credit Card", "Debit Card", "Bank Transfer" };
            int selection = this._consoleHelper.ReadSelection("Select payment method:", choices);
            return selection == 1 ? null : (PaymentMethod)(selection - 2);
        }

        /// <summary>
        /// Selects an optional transaction category.
        /// </summary>
        /// <param name="flowType">The flow type filter context.</param>
        /// <returns>The chosen TransactionCategory, or null for Any.</returns>
        private TransactionCategory? GetOptionalCategory(TransactionType? flowType)
        {
            List<string> choices = new List<string> { "Any" };
            List<TransactionCategory> categories = new List<TransactionCategory>();

            if (!flowType.HasValue || flowType.Value == TransactionType.Income)
            {
                categories.Add(TransactionCategory.Salary);
                categories.Add(TransactionCategory.Investment);
                categories.Add(TransactionCategory.MiscellaneousIncome);
            }

            if (!flowType.HasValue || flowType.Value == TransactionType.Expense)
            {
                categories.Add(TransactionCategory.Transport);
                categories.Add(TransactionCategory.Utilities);
                categories.Add(TransactionCategory.Groceries);
                categories.Add(TransactionCategory.Rent);
                categories.Add(TransactionCategory.Food);
                categories.Add(TransactionCategory.Shopping);
                categories.Add(TransactionCategory.MiscellaneousExpense);
            }

            choices.AddRange(categories.Select(c => c.ToString()));
            int selection = this._consoleHelper.ReadSelection("Select category:", choices);
            return selection == 1 ? null : categories[selection - 2];
        }

        /// <summary>
        /// Validates that a category belongs to a given flow type.
        /// </summary>
        private bool IsValidCategoryCombination(TransactionType type, TransactionCategory category)
        {
            if (type == TransactionType.Income)
            {
                return category == TransactionCategory.Salary ||
                       category == TransactionCategory.Investment ||
                       category == TransactionCategory.MiscellaneousIncome;
            }

            if (type == TransactionType.Expense)
            {
                return category == TransactionCategory.Transport ||
                       category == TransactionCategory.Utilities ||
                       category == TransactionCategory.Groceries ||
                       category == TransactionCategory.Rent ||
                       category == TransactionCategory.Food ||
                       category == TransactionCategory.Shopping ||
                       category == TransactionCategory.MiscellaneousExpense;
            }

            return false;
        }

        /// <summary>
        /// Maps an Expense/Income category to a unique color for beautiful charts.
        /// </summary>
        private Color GetCategoryColor(TransactionCategory category)
        {
            return category switch
            {
                TransactionCategory.Salary => Color.Green,
                TransactionCategory.Investment => Color.Teal,
                TransactionCategory.MiscellaneousIncome => Color.GreenYellow,
                TransactionCategory.Transport => Color.SkyBlue1,
                TransactionCategory.Utilities => Color.Purple,
                TransactionCategory.Groceries => Color.DarkOrange,
                TransactionCategory.Rent => Color.Red,
                TransactionCategory.Food => Color.Yellow,
                TransactionCategory.Shopping => Color.Purple,
                TransactionCategory.MiscellaneousExpense => Color.Grey,
                _ => Color.White
            };
        }
    }
}
