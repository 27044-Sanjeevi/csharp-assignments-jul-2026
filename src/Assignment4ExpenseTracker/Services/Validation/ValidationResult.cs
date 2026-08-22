namespace Assignment4ExpenseTracker.Services.Validation
{
    /// <summary>
    /// Result pattern for validation of the transactions.
    /// </summary>
    internal class ValidationResult
    {
        /// <summary>
        /// Stores the collection of error messages.
        /// </summary>
        private readonly List<string> _errors = new List<string>();

        /// <summary>
        /// Gets a value indicating whether the validation succeeded.
        /// </summary>
        /// <value>true if the validation contains no error; otherwise, false.</value>
        public bool IsValid => this._errors.Count == 0;

        /// <summary>
        /// Gets the collection of error messages.
        /// </summary>
        /// <value>The list of errors during the validation of a transaction.</value>
        public IEnumerable<string> Errors => this._errors;

        /// <summary>
        /// Adds a new error message to the result.
        /// </summary>
        /// <param name="errorMessage">The error message details.</param>
        public void AddError(string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                this._errors.Add(errorMessage);
            }
        }
    }
}
