using System;
using System.Collections.Generic;
using Assignment5ExpenseTrackerEnhanced.Models;
using Assignment5ExpenseTrackerEnhanced.Models.DTOs;
using Assignment5ExpenseTrackerEnhanced.Models.Enums;
using Assignment5ExpenseTrackerEnhanced.Persistence;
using Assignment5ExpenseTrackerEnhanced.Services.Validation;

namespace Assignment5ExpenseTrackerEnhanced.Services
{
    /// <summary>
    /// Provides the business logic for the transactions in the expense tracker application.
    /// </summary>
    internal class TransactionService : ITransactionService
    {
        private readonly IRepository _repository;
        private readonly TransactionValidator _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="repository">The data persistence repository access tier.</param>
        /// <param name="validator">The engine used to enforce data rules.</param>
        public TransactionService(IRepository repository, TransactionValidator validator)
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
                Timestamp = DateTime.Now,
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
                Timestamp = updateDto.Timestamp,
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
        public IReadOnlyList<Transaction> GetAllTransactions()
        {
            return this._repository.GetAll();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> SearchTransactions(TransactionType? type, TransactionCategory? category, PaymentMethod? method, string? keyword)
        {
            IEnumerable<Transaction> query = this._repository.GetAll();

            if (type.HasValue)
            {
                query = query.Where(t => t.Type == type.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(t => t.Category == category.Value);
            }

            if (method.HasValue)
            {
                query = query.Where(t => t.Method == method.Value);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(t => t.Description != null
                                         && t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> GetSortedTransactions(SortBy sortBy, SortOrder order)
        {
            IEnumerable<Transaction> transactions = this._repository.GetAll();

            switch (sortBy)
            {
                case SortBy.Date:
                    transactions = order == SortOrder.Ascending
                        ? transactions.OrderBy(t => t.Timestamp)
                        : transactions.OrderByDescending(t => t.Timestamp);
                    break;
                case SortBy.Amount:
                    transactions = order == SortOrder.Ascending
                        ? transactions.OrderBy(t => t.Amount)
                        : transactions.OrderByDescending(t => t.Amount);
                    break;
                case SortBy.Category:
                    transactions = order == SortOrder.Ascending
                        ? transactions.OrderBy(t => t.Category.ToString())
                        : transactions.OrderByDescending(t => t.Category.ToString());
                    break;
            }

            return transactions.ToList();
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByTransactionType(TransactionType type)
        {
            return this._repository.FilterByTransactionType(type);
        }

        /// <inheritdoc />
        public IReadOnlyList<Transaction> FilterByCategory(TransactionCategory category)
        {
            return this._repository.FilterByCategory(category);
        }

        /// <inheritdoc />
        public int GetTransactionCount()
        {
            return this._repository.GetTransactionCount();
        }

        /// <inheritdoc />
        public ReportDto GenerateFinancialReport()
        {
            IReadOnlyList<Transaction> transactions = this._repository.GetAll().ToList();
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    totalIncome += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Expense)
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
