using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.DTOs;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;
using Assignment5ExpenseTrackerEnhanced.Services;
using Assignment5ExpenseTrackerEnhanced.Services.Validation;
using Assignment5ExpenseTrackerEnhanced.View;

namespace Assignment5ExpenseTrackerEnhanced.Controller
{
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
                    this.Filter();
                    break;
                case 6:
                    this.Sort();
                    break;
                case 7:
                    this.Search();
                    break;
                case 8:
                    this.DisplayReport();
                    break;
                case 9:
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
                Timestamp = selectedRecord.Timestamp,
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

            if (!this.TryGetSelectedTransaction(out Transaction? selectedRecord) || selectedRecord == null)
            {
                return;
            }

            if (this._consoleView.ConfirmDelete(selectedRecord))
            {
                ValidationResult result = this._transactionService.DeleteTransaction(selectedRecord.Id);
                if (result.IsValid)
                {
                    this._consoleView.DisplayDeleteSuccessful();
                }
                else
                {
                    this._consoleView.DisplayValidationResult(result);
                }
            }
        }

        /// <inheritdoc />
        public void Search()
        {
            this._consoleView.DisplaySearchHeader();
            IReadOnlyList<Transaction> results = new List<Transaction>();

            if (this.GetAllTransactionsCount() <= 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            string keyword = this._consoleView.GetSearchKeyword();
            results = this._transactionService.GetAllTransactions().Where(t =>
            t.Timestamp.ToString("yyyy-MM-dd HH:mm").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (t.Description != null && t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
            t.Amount.ToString().Equals(keyword) ||
            t.Category.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            t.Type.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            t.Method.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            this._consoleView.DisplayAsTable(results);
        }

        /// <inheritdoc />
        public void Sort()
        {
            (SortBy sortBy, SortOrder order) = this._consoleView.GetSortingCriteria();
            IReadOnlyList<Transaction> results = this._transactionService.GetSortedTransactions(sortBy, order);

            this._consoleView.DisplayAllTransactionsHeader();
            if (results == null || results.Count == 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            this._consoleView.DisplayAsTable(results);
        }

        /// <inheritdoc />
        public void Filter()
        {
            this._consoleView.DisplayFilterHeader();
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();
            if (transactions.Count <= 0)
            {
                this._consoleView.DisplayTransactionsNotFound();
                return;
            }

            FilterType filterType = this._consoleView.GetFilterTypeChoice();
            TransactionType transactionType = this._consoleView.GetTransactionTypeChoice();
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

        /// <summary>
        /// Retrieves the count of transactions in the repository.
        /// </summary>
        /// <returns>An integer representing the count of transactions in the repository.</returns>
        private int GetAllTransactionsCount()
        {
            return this._transactionService.GetTransactionCount();
        }

        /// <summary>
        /// Reusable flow helper to retrieve a selected transaction by index and perform validation.
        /// </summary>
        /// <param name="selectedRecord">The retrieved transaction if found; otherwise, null.</param>
        /// <returns>True if a transaction was successfully selected; otherwise, false.</returns>
        private bool TryGetSelectedTransaction(out Transaction? selectedRecord)
        {
            selectedRecord = this.GetTransactionByIndex();
            return selectedRecord != null;
        }
    }
}
