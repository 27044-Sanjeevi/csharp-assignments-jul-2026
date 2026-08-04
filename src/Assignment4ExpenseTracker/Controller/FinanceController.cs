namespace Assignment4ExpenseTracker.Controller
{
    using System.ComponentModel;
    using System.Reflection;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.DTOs;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Services;
    using Assignment4ExpenseTracker.Services.Validation;
    using Assignment4ExpenseTracker.View;

    /// <summary>
    /// Coordinates operations between the View and the Service layer.
    /// </summary>
    internal class FinanceController
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

        /// <summary>
        /// Processes a transaction menu selection and invokes the corresponding operation.
        /// </summary>
        /// <param name="choice">The menu option selected for transaction operations.</param>
        /// <returns>true if the user chose to exit; otherwise, false.</returns>
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
                    return true;
                case 6:
                    // this.GenerateReport();
                    break;
                case 7:
                    // this.ExitApplication();
                    return true;
                default:
                    // this.DisplayInvalidChoiceMessage();
                    break;
            }

            return false;
        }

        /// <summary>
        /// Adds a transaction using user-provided details and displays the validation result.
        /// </summary>
        public void AddTransaction()
        {
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
            this._consoleView.DisplayValidationResult(result);
        }

        /// <summary>
        /// Updates an existing transaction based on user input choices mapped through a DTO.
        /// </summary>
        public void UpdateTransaction()
        {
            Transaction? selectedRecord = this.GetTransactionByIndex();
            if (selectedRecord == null)
            {
                return;
            }

            decimal amount = this._consoleView.GetTransactionAmountToUpdate(selectedRecord.Amount);
            FlowType flowType = this._consoleView.GetFlowChoice();
            PaymentMethod paymentMethod = this._consoleView.GetPaymentMethod();
            TransactionCategory category = flowType == FlowType.Income ?
                                           this._consoleView.GetIncomeCategory() :
                                           this._consoleView.GetExpenseCategory();
            string? description = this._consoleView.GetTransactionDescriptionToUpdate(selectedRecord.Description);

            TransactionUpdateDto updatedTransaction = new TransactionUpdateDto
            {
                Id = selectedRecord.Id,
                Amount = amount,
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

        /// <summary>
        /// Filters transactions based on the selected filter type and flow type.
        /// </summary>
        public void FilterTransactions()
        {
            FilterType filterType = this._consoleView.GetFilterTypeChoice();
            IReadOnlyList<Transaction> transactions = new List<Transaction>();
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

        /// <summary>
        /// Displays all the transactions as table.
        /// </summary>
        public void ViewAllTransactions()
        {
            IReadOnlyList<Transaction> transactions = this._transactionService.GetAllTransactions();

            this._consoleView.DisplayAsTable(transactions);
        }

        /// <summary>
        /// Deletes an existing transaction from the repository.
        /// </summary>
        public void DeleteTransaction()
        {
            this._consoleView.DisplayDeleteHeader();
            this.ViewAllTransactions();

            Transaction? selectedRecord = this.GetTransactionByIndex();

            if (selectedRecord == null)
            {
                return;
            }

            this._transactionService.DeleteTransaction(selectedRecord.Id);
        }

        /// <summary>
        /// Retrieves the index of a selected transaction from the displayed transaction list.
        /// </summary>
        /// <remarks>Displays a message if no transactions are available.</remarks>
        /// <returns>The index of the selected transaction.</returns>
        private Transaction? GetTransactionByIndex()
        {
            List<Transaction> transactions = this._transactionService.GetAllTransactions();

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
