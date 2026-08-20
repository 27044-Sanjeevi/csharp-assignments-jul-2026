namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Handles Task 2: Catching and Throwing Different Types of Exceptions.
    /// </summary>
    internal class Task2Controller : ITaskController
    {
        private readonly ConsoleView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task2Controller"/> class.
        /// </summary>
        /// <param name="view">The console view for UI operations.</param>
        public Task2Controller(ConsoleView view)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <inheritdoc />
        public void RunTask()
        {
            this._view.PrintHeader("Task 2: Catching & Throwing Exceptions");

            int[] numbers = { 10, 20, 30, 40, 50 };

            try
            {
                this.AccessArrayElement(numbers, 10);
            }
            catch (Exception wrapperEx)
            {
                this._view.WriteColored($"\n[OUTER CATCH] Caught the wrapped exception: {wrapperEx.Message}\n", ConsoleColor.Green);
                if (wrapperEx.InnerException != null)
                {
                    this._view.WriteLine($"Inner exception: {wrapperEx.InnerException.GetType().Name} - {wrapperEx.InnerException.Message}");
                }
            }
        }

        private void AccessArrayElement(int[] array, int index)
        {
            try
            {
                this._view.WriteLine($"Trying to access element at index {index} of array (size: {array.Length})");
                int val = array[index];
                this._view.WriteLine($"Value: {val}");
            }
            catch (IndexOutOfRangeException ex)
            {
                this._view.WriteColored($"\n[INNER CATCH] Caught exception: {ex.Message}", ConsoleColor.Red);
                throw new InvalidOperationException("Custom Message: Array indexing access operation failed due to invalid bounds.", ex);
            }
        }
    }
}
