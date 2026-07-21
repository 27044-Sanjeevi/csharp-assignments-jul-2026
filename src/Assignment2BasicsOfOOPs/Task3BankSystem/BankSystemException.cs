namespace Assignment2BasicsOfOOPs.Task3BankSystem
{
    /// <summary>
    /// Represent the exception mechanism in the Bank System
    /// </summary>
    internal class BankSystemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankSystemException"/> class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public BankSystemException(string message)
            : base(message)
        {
            // empty body, the error printing is handled in the base class
        }
    }
}
