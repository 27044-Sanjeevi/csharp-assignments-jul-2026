namespace Assignment4ExpenseTracker.Controller
{
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.DTOs;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Services;
    using Assignment4ExpenseTracker.Services.Validation;
    using Assignment4ExpenseTracker.View;

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
        public bool HandleTransactionMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.AddTransaction();
                    break;
                case 2:
                    this.ViewAllTransactions();
                    break;
                case 3:
                    this.UpdateTransaction();
                    break;
                case 4:
                    this.DeleteTransaction();
                    break;
                case 5:
                    this.GenerateReport();
                    break;
                case 6:
                    return true;
                default:
                    break;
            }

            return false;
        }

        /// <inheritdoc />
        public void AddTransaction()
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
                this._consoleView.DisplaySuccessfulAdd();
            }
        }

        /// <inheritdoc />
        public void UpdateTransaction()
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
        public void ViewAllTransactions()
        {
            this._consoleView.DisplayAllTransactionsHeader();
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();

            this._consoleView.DisplayAsTable(transactions);
        }

        /// <inheritdoc />
        public void DeleteTransaction()
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
        public void GenerateReport()
        {
            this._consoleView.DisplayReportHeader();
            ReportDto report = this._transactionService.GenerateFinancialReport();
            this._consoleView.DisplayInsights(
                report.TotalIncome,
                report.TotalExpense,
                report.NetBalance,
                report.TransactionCount);
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
