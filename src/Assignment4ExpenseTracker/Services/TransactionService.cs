namespace Assignment4ExpenseTracker.Services
{
    using System;
    using System.Collections.Generic;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.DTOs;
    using Assignment4ExpenseTracker.Models.Enums;
    using Assignment4ExpenseTracker.Persistence;
    using Assignment4ExpenseTracker.Services.Validation;

    /// <summary>
    /// Represents the service layer of the transactions in the expense tracker application.
    /// </summary>
    internal class TransactionService : ITransactionService
    {
        private readonly IRepository _repository;
        private readonly ITransactionValidation _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="repository">The data persistence repository access tier.</param>
        /// <param name="validator">The engine used to enforce data rules.</param>
        public TransactionService(IRepository repository, ITransactionValidation validator)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <inheritdoc />
        public ValidationResult CreateTransaction(TransactionInputDto transactionDto)
        {
            ArgumentNullException.ThrowIfNull(transactionDto, nameof(transactionDto));

            Transaction transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                Type = transactionDto.Type,
                Category = transactionDto.Category,
                Method = transactionDto.Method,
                Description = transactionDto.Description,
                TimeStamp = DateTime.Now,
            };

            ValidationResult validationResult = this._validator.ValidateTransaction(transaction);

            if (validationResult.IsValid)
            {
                this._repository.Add(transaction);
            }

            return validationResult;
        }

        /// <inheritdoc />
        public ValidationResult DeleteTransaction(Guid id)
        {
            ValidationResult validationResult = this._validator.ValidateDeletion(id);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!this._repository.Delete(id))
            {
                validationResult.AddError("Transaction with the specified identifier does not exist.");
            }

            return validationResult;
        }

        /// <inheritdoc />
        public ValidationResult UpdateTransaction(TransactionUpdateDto updateDto)
        {
            ArgumentNullException.ThrowIfNull(updateDto, nameof(updateDto));

            ValidationResult validationResult = new ValidationResult();

            Transaction updatedModel = new Transaction(updateDto.Id)
            {
                Amount = updateDto.Amount,
                Type = updateDto.Type,
                Category = updateDto.Category,
                Method = updateDto.Method,
                Description = updateDto.Description,
                TimeStamp = updateDto.TimeStamp,
            };

            validationResult = this._validator.ValidateTransaction(updatedModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!this._repository.Update(updatedModel))
            {
                validationResult.AddError("Transaction with the specified identifier does not exist.");
            }

            return validationResult;
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByFlowType(FlowType type)
        {
            return this._repository.FilterByFlowType(type);
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category)
        {
            return this._repository.FilterByCategory(category);
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> GetAllTransactions()
        {
            return this._repository.GetAll().ToList();
        }

        /// <inheritdoc />
        public ReportDto GenerateFinancialReport()
        {
            IReadOnlyList<Transaction> transactions = this._repository.GetAll().ToList();
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Type == FlowType.Income)
                {
                    totalIncome += transaction.Amount;
                }
                else if (transaction.Type == FlowType.Expense)
                {
                    totalExpense += transaction.Amount;
                }
            }

            return new ReportDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetBalance = totalIncome - totalExpense,
                TransactionCount = transactions.Count,
            };
        }
    }
}
