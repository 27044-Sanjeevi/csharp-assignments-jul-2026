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
        public bool HandleMenu(MainMenuOption choice)
        {
            switch (choice)
            {
                case MainMenuOption.Add: this.Add(); break;
                case MainMenuOption.ViewAll: this.ViewAll(); break;
                case MainMenuOption.Update: this.Update(); break;
                case MainMenuOption.Delete: this.Delete(); break;
                case MainMenuOption.GenerateReport: this.GenerateReport(); break;
                case MainMenuOption.Exit: return true;
                default: break;
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
            if (!this.TryGetSelectedTransaction(out Transaction? selectedRecord) || selectedRecord == null)
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

            if (!result.IsValid)
            {
                this._consoleView.DisplayValidationResult(result);
            }
            else
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
