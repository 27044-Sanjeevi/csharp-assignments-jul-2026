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
    internal class FinanceController : IFinanceController
    {
        private readonly ITransactionService _transactionService;
        private readonly IView _consoleView;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceController"/> class.
        /// </summary>
        /// <param name="transactionService">The service for transaction related operations.</param>
        /// <param name="consoleView">The console view renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is null.</exception>
        public FinanceController(ITransactionService transactionService, IView consoleView)
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
                    this.FilterTransactions();
                    break;
                case 6:
                    this.GenerateReport();
                    break;
                case 7:
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
            FlowType flowType = this._consoleView.GetFlowChoice();
            PaymentMethod paymentMethod = this._consoleView.GetPaymentMethod();
            TransactionCategory category = flowType == FlowType.Income ?
                                           this._consoleView.GetIncomeCategory() :
                                           this._consoleView.GetExpenseCategory();
            string? description = this._consoleView.GetTransactionDescription();

            TransactionInputDto transactionDto = new TransactionInputDto
            {
                Amount = amount,
                Type = flowType,
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
            FlowType flowType = this._consoleView.GetFlowChoice(selectedRecord.Type);
            PaymentMethod paymentMethod = this._consoleView.GetPaymentMethod(selectedRecord.Method);
            TransactionCategory category;
            if (flowType == selectedRecord.Type)
            {
                category = flowType == FlowType.Income ?
                           this._consoleView.GetIncomeCategory(selectedRecord.Category) :
                           this._consoleView.GetExpenseCategory(selectedRecord.Category);
            }
            else
            {
                category = flowType == FlowType.Income ?
                           this._consoleView.GetIncomeCategory() :
                           this._consoleView.GetExpenseCategory();
            }

            string? description = this._consoleView.GetTransactionDescriptionToUpdate(selectedRecord.Description);

            TransactionUpdateDto updatedTransaction = new TransactionUpdateDto
            {
                Id = selectedRecord.Id,
                Amount = amount,
                TimeStamp = selectedRecord.TimeStamp,
                Type = flowType,
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
            FlowType flowType = this._consoleView.GetFlowChoice();
            if (filterType == FilterType.FlowType)
            {
                transactions = this._transactionService.FilterByFlowType(flowType);
            }

            if (filterType == FilterType.Category)
            {
                TransactionCategory category = flowType == FlowType.Income ?
                                           this._consoleView.GetIncomeCategory() :
                                           this._consoleView.GetExpenseCategory();
                transactions = this._transactionService.FilterByCategory(category);
            }

            this._consoleView.DisplayFilteredTable(transactions);
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
