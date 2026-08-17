namespace Assignment5ExpenseTrackerEnhanced.Controller
{
    using Assignment5ExpenseTrackerEnhanced.Models;
    using Assignment5ExpenseTrackerEnhanced.Models.DTOs;
    using Assignment5ExpenseTrackerEnhanced.Models.Enums;
    using Assignment5ExpenseTrackerEnhanced.Services;
    using Assignment5ExpenseTrackerEnhanced.Services.Validation;
    using Assignment5ExpenseTrackerEnhanced.View;

    /// <summary>
    /// Coordinates operations between the View and the Service layer.
    /// </summary>
    internal class TransactionController : ITransactionController
    {
        private readonly ITransactionService _transactionService;
        private readonly IView _consoleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="transactionService">The service for transaction related operations.</param>
        /// <param name="consoleView">The console view renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is null.</exception>
        public TransactionController(ITransactionService transactionService, IView consoleView)
        {
            this._transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            this._consoleView = consoleView ?? throw new ArgumentNullException(nameof(consoleView));
        }

        /// <inheritdoc />
        public bool HandleMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.Add();
                    break;
                case 2:
                    this.ViewAll();
                    break;
                case 3:
                    this.Update();
                    break;
                case 4:
                    this.Delete();
                    break;
                case 5:
                    this.Search();
                    break;
                case 6:
                    this.FilterTransactions();
                    break;
                case 7:
                    this.Sort();
                    break;
                case 8:
                    this.DisplayReport();
                    break;
                case 9:
                    // this.GenerateReportFile();
                    break;
                case 10:
                    return true;
                default:
                    break;
            }

            return false;
        }

        /// <inheritdoc />
        public void Add()
        {
            this._consoleView.DisplayAddHeader();
            decimal amount = this._consoleView.GetTransactionAmount();
            TransactionType transactionType = this._consoleView.GetTransactionTypeChoice();
            PaymentMethod paymentMethod = this._consoleView.GetPaymentMethod();
            TransactionCategory category = transactionType == TransactionType.Income ?
                                           this._consoleView.GetIncomeCategory() :
                                           this._consoleView.GetExpenseCategory();
            string? description = this._consoleView.GetTransactionDescription();

            TransactionInputDto transactionDto = new TransactionInputDto
            {
                Amount = amount,
                Type = transactionType,
                Category = category,
                Method = paymentMethod,
                Description = description,
            };

            ValidationResult result = this._transactionService.CreateTransaction(transactionDto);

            if (!result.IsValid)
            {
                this._consoleView.DisplayValidationResult(result);
            }
            else
            {
                this._consoleView.DisplayAddSuccessful();
            }
        }

        /// <inheritdoc />
        public void Update()
        {
            this._consoleView.DisplayUpdateHeader();
            Transaction? selectedRecord = this.GetTransactionByIndex();
            if (selectedRecord == null)
            {
                return;
            }

            decimal amount = this._consoleView.GetTransactionAmountToUpdate(selectedRecord.Amount);
            TransactionType transactionType = this._consoleView.GetTransactionTypeChoice(selectedRecord.Type);
            PaymentMethod paymentMethod = this._consoleView.GetPaymentMethod(selectedRecord.Method);
            TransactionCategory category;
            if (transactionType == selectedRecord.Type)
            {
                category = transactionType == TransactionType.Income ?
                           this._consoleView.GetIncomeCategory(selectedRecord.Category) :
                           this._consoleView.GetExpenseCategory(selectedRecord.Category);
            }
            else
            {
                category = transactionType == TransactionType.Income ?
                           this._consoleView.GetIncomeCategory() :
                           this._consoleView.GetExpenseCategory();
            }

            string? description = this._consoleView.GetTransactionDescriptionToUpdate(selectedRecord.Description);

            TransactionUpdateDto updatedTransaction = new TransactionUpdateDto
            {
                Id = selectedRecord.Id,
                Amount = amount,
                TimeStamp = selectedRecord.TimeStamp,
                Type = transactionType,
                Category = category,
                Method = paymentMethod,
                Description = description,
            };

            ValidationResult result = this._transactionService.UpdateTransaction(updatedTransaction);
            this._consoleView.DisplayValidationResult(result);

            if (result.IsValid)
            {
                this._consoleView.DisplayUpdateSuccessful();
            }
        }

        /// <inheritdoc />
        public void ViewAll()
        {
            this._consoleView.DisplayAllTransactionsHeader();
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();
            if (transactions == null || transactions.Count == 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            this._consoleView.DisplayAsTable(transactions);
        }

        /// <inheritdoc />
        public void Delete()
        {
            this._consoleView.DisplayDeleteHeader();

            Transaction? selectedRecord = this.GetTransactionByIndex();

            if (selectedRecord == null)
            {
                return;
            }

            this._transactionService.DeleteTransaction(selectedRecord.Id);
            this._consoleView.DisplayDeleteSuccessful();
        }

        /// <inheritdoc />
        public void Search()
        {
            (TransactionType? type, TransactionCategory? category, PaymentMethod? method, string? keyword, bool isCancelled) = this._consoleView.GetSearchCriteria();

            if (isCancelled)
            {
                return;
            }

            IReadOnlyList<Transaction> results = this._transactionService.SearchTransactions(type, category, method, keyword);

            this._consoleView.DisplayAllTransactionsHeader();
            if (results == null || results.Count == 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            this._consoleView.DisplayAsTable(results);
        }

        /// <inheritdoc />
        public void Sort()
        {
            (SortBy sortBy, bool ascending) = this._consoleView.GetSortingCriteria();
            IReadOnlyList<Transaction> results = this._transactionService.GetSortedTransactions(sortBy, ascending);

            this._consoleView.DisplayAllTransactionsHeader();
            if (results == null || results.Count == 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            this._consoleView.DisplayAsTable(results);
        }

        /// <inheritdoc />
        public void FilterTransactions()
        {
            this._consoleView.DisplayFilterHeader();
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();
            if (transactions.Count <= 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            FilterType filterType = this._consoleView.GetFilterTypeChoice();
            TransactionType transactionType = this._consoleView.GetTransactionType();
            if (filterType == FilterType.TransactionType)
            {
                transactions = this._transactionService.FilterByTransactionType(transactionType);
            }

            if (filterType == FilterType.Category)
            {
                TransactionCategory category = transactionType == TransactionType.Income ?
                                           this._consoleView.GetIncomeCategory() :
                                           this._consoleView.GetExpenseCategory();
                transactions = this._transactionService.FilterByCategory(category);
            }

            this._consoleView.DisplayFilteredTable(transactions);
        }

        /// <inheritdoc />
        public void DisplayReport()
        {
            this._consoleView.DisplayReportHeader();
            ReportDto report = this._transactionService.GenerateFinancialReport();
            this._consoleView.DisplayInsights(
                report.TotalIncome,
                report.TotalExpense,
                report.NetBalance,
                report.TransactionCount);

            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();
            this._consoleView.DisplayVisualCharts(transactions);
        }

        /// <summary>
        /// Retrieves the index of a selected transaction from the displayed transaction list.
        /// </summary>
        /// <remarks>Displays a message if no transactions are available.</remarks>
        /// <returns>The index of the selected transaction.</returns>
        private Transaction? GetTransactionByIndex()
        {
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();

            if (transactions == null || transactions.Count == 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return null;
            }

            this._consoleView.DisplayAsTable(transactions);

            int targetIndex = this._consoleView.GetIndexFromTable(transactions.Count);
            return transactions[targetIndex];
        }
    }
}
