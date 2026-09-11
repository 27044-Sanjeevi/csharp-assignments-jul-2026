namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Handles Task 1 of involving divisionByZero Exception.
    /// </summary>
    internal class Task1Controller : ITaskController
    {
        private readonly ConsoleView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task1Controller"/> class.
        /// </summary>
        /// <param name="view">The console view for UI operations.</param>
        public Task1Controller(ConsoleView view)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <inheritdoc />
        public void RunTask()
        {
            this._view.PrintHeader("Task 1: Division & try/catch/finally");

            int numerator = 10;
            int denominator = 0;

            this._view.WriteLine($"\nPerforming operation: {numerator} / {denominator}");

            try
            {
                int result = numerator / denominator;
                this._view.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                this._view.WriteColored($"\n[CATCH] Caught DivideByZeroException: {ex.Message}\n", ConsoleColor.Red);
            }
            finally
            {
                this._view.WriteColored("\n[FINALLY] The finally block has been executed.\n", ConsoleColor.Green);
            }
        }
    }
}
