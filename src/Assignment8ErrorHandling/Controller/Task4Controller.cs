namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Handles Task 4: Handling Global Unhandled Exceptions.
    /// </summary>
    internal class Task4Controller : ITaskController
    {
        private readonly ConsoleView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task4Controller"/> class.
        /// </summary>
        /// <param name="view">The console view for UI operations.</param>
        public Task4Controller(ConsoleView view)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <inheritdoc />
        public void RunTask()
        {
            this._view.PrintHeader("Task 4: Global Unhandled Exceptions");
            string? choice = this._view.ReadLine("Do you want to throw the unhandled exception? (y/n): ");

            if (choice?.Trim().ToLower() == "y")
            {
                this._view.WriteLine("\nCalling method that invokes Unhandled exception.");
                this.CauseUnhandledException();
            }
            else
            {
                this._view.WriteLine("\nTask aborted. No unhandled exception was thrown.");
            }
        }

        /// <summary>
        /// Throws an unhandled exception.
        /// </summary>
        public void ThrowUnhandledException()
        {
            string? choice = this._view.ReadLine("Do you want to throw the unhandled exception? (y/n): ");

            if (choice?.Trim().ToLower() == "y")
            {
                this._view.WriteLine("\nCalling method that invokes Unhandled exception.");
                this.CauseUnhandledException();
            }
            else
            {
                this._view.WriteLine("\nTask aborted. No unhandled exception was thrown.");
            }
        }

        /// <summary>
        /// A method that intentionally throws an exception without handling it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Always thrown to demonstrate unhandled exceptions.</exception>
        public void CauseUnhandledException()
        {
            throw new InvalidOperationException("This exception is thrown by CauseUnhandledException and is unhandled.");
        }
    }
}
