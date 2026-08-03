namespace Assignment4ExpenseTracker.Services
{
    using System;
    using System.Collections.Generic;
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.DTOs;
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
                this._validator.AppendIdNotFoundError(validationResult);
            }

            return validationResult;
        }

        /// <inheritdoc />
        public ValidationResult UpdateTransaction(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
            ValidationResult validationResult = this._validator.ValidateTransaction(transaction);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!this._repository.Update(transaction))
            {
                this._validator.AppendIdNotFoundError(validationResult);
            }

            return validationResult;
        }

        /// <inheritdoc />
        public List<Transaction> GetAllTransactions()
        {
            return this._repository.GetAll().ToList();
        }
    }
}
