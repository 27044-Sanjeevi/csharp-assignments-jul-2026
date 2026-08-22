namespace Assignment8ErrorHandling.Exceptions
{
    using System;

    /// <summary>
    /// Represents errors that occur when the user provides invalid input.
    /// </summary>
    internal class InvalidUserInputException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class.
        /// </summary>
        public InvalidUserInputException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUserInputException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidUserInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
