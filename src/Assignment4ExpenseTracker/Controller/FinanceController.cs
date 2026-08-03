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
        public void HandleTransactionMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.AddTransaction();
                    break;
                case 2:
                    this.UpdateTransaction();
                    break;
                case 3:
                    this.DeleteTransaction();
                    break;
                case 4:
                    this.ViewAllTransactions();
                    break;
                case 5:
                    this.ViewTransactionById();
                    break;
                default:
                    this.DisplayInvalidChoiceMessage();
                    break;
            }
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
        /// Updates a transaction with user-provided details and displays the validation outcome.
        /// </summary>
        public void UpdateTransaction()
        {
            decimal amount = this._consoleView.GetTransactionAmountToUpdate();

            Transaction transaction = this._consoleView.GetTransactionDetailsToUpdate();
            ValidationResult result = this._transactionService.UpdateTransaction(transaction);
            this._consoleView.DisplayValidationResult(result);
        }
    }
}
