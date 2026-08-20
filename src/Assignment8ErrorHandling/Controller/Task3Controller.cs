namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.Models.Exceptions;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Handles Task 3: Defining and Using Custom Exception Classes.
    /// </summary>
    internal class Task3Controller : ITaskController
    {
        private readonly ConsoleView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task3Controller"/> class.
        /// </summary>
        /// <param name="view">The console view for UI operations.</param>
        public Task3Controller(ConsoleView view)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <inheritdoc />
        public void RunTask()
        {
            this._view.PrintHeader("Task 3: Custom Exception Class (User Array Input)");
            try
            {
                int size = this._view.ReadInt("Enter the size of the array: ");
                int[] numbers = new int[size];
                for (int i = 0; i < size; i++)
                {
                    numbers[i] = this._view.ReadInt($"Enter an integer element at index {i}: ");
                }

                int index = this._view.ReadInt($"Enter index to access (0 to {size - 1}): ");
                this._view.WriteLine($"Accessing index {index}");
                int value = numbers[index];
                this._view.WriteColored($"\n[SUCCESS] The value at index {index} is: {value}\n", ConsoleColor.Green);
            }
            catch (InvalidUserInputException ex)
            {
                this._view.WriteColored($"\n[CATCH] Caught Custom Exception (InvalidUserInputException): {ex.Message}\n", ConsoleColor.Red);
            }
            catch (IndexOutOfRangeException ex)
            {
                this._view.WriteColored($"\n[CATCH] Caught Exception (IndexOutOfRangeException): {ex.Message}\n", ConsoleColor.Red);
            }
        }
    }
}
