namespace Assignment4ExpenseTracker.Services.Validation
{
    using Assignment4ExpenseTracker.Models;
    using Assignment4ExpenseTracker.Models.Enums;

    /// <summary>
    /// Provides validation logic for transaction objects to ensure data integrity and adherence to business rules.
    /// </summary>
    internal class TransactionValidation
    {
        private const string NullTransactionMessage = "Transaction data cannot be null.";
        private const string InvalidAmountMessage = "Amount must be greater than zero.";
        private const string FutureDateMessage = "Date cannot be in the future.";
        private const string InvalidDescriptionLength = "Description cannot exceed 50 characters.";
        private const string InvalidCategoryMessage = "Invalid category for the given flow type.";
        private const string InvalidDeletionIdMessage = "A valid transaction identifier must be provided for deletion.";
        private const int MaxDescriptionLength = 50;

        /// <summary>
        /// Validates the given transaction object to ensure it meets the required criteria.
        /// </summary>
        /// <param name="transaction">The transaction object to be validated.</param>
        /// <returns>The validation result object.</returns>
        public ValidationResult ValidateTransaction(Transaction transaction)
        {
            ValidationResult validationResult = new ValidationResult();

            if (transaction is null)
            {
                validationResult.AddError(NullTransactionMessage);
                return validationResult;
            }

            if (transaction.Amount <= 0)
            {
                validationResult.AddError(InvalidAmountMessage);
            }

            if (transaction.Timestamp > DateTime.Now)
            {
                validationResult.AddError(FutureDateMessage);
            }

            if (transaction.Description is not null
                && transaction.Description.Length > MaxDescriptionLength)
            {
                validationResult.AddError(InvalidDescriptionLength);
            }

            if (this.IsValidCategoryCombination(transaction.Type, transaction.Category) == false)
            {
                validationResult.AddError(InvalidCategoryMessage);
            }

            return validationResult;
        }

        /// <summary>
        /// Validates a deletion request based on the transaction record identifier.
        /// </summary>
        /// <param name="id">The transaction identifier to evaluate.</param>
        /// <returns>A validation result container.</returns>
        public ValidationResult ValidateDeletion(Guid id)
        {
            ValidationResult validationResult = new ValidationResult();
            if (id == Guid.Empty)
            {
                validationResult.AddError(InvalidDeletionIdMessage);
            }

            return validationResult;
        }

        private bool IsValidCategoryCombination(TransactionType type, TransactionCategory category)
        {
            if (type == TransactionType.Income)
            {
                return category == TransactionCategory.Salary ||
                       category == TransactionCategory.Investment ||
                       category == TransactionCategory.Freelance ||
                       category == TransactionCategory.Business ||
                       category == TransactionCategory.Gifts ||
                       category == TransactionCategory.MiscellaneousIncome;
            }

            if (type == TransactionType.Expense)
            {
                return category == TransactionCategory.Transport ||
                       category == TransactionCategory.Utilities ||
                       category == TransactionCategory.Groceries ||
                       category == TransactionCategory.Rent ||
                       category == TransactionCategory.Food ||
                       category == TransactionCategory.Shopping ||
                       category == TransactionCategory.Healthcare ||
                       category == TransactionCategory.Education ||
                       category == TransactionCategory.MiscellaneousExpense;
            }

            return false;
        }
    }
}
