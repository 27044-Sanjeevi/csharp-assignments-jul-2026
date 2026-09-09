namespace Assignment8ErrorHandling.Controller
{
    using System;
    using Assignment8ErrorHandling.View;

    /// <summary>
    /// Handles Task 5: Stack Trace Interpretation.
    /// </summary>
    internal class Task5Controller : ITaskController
    {
        private readonly ConsoleView _view;
        private readonly ITaskController _task4Controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task5Controller"/> class.
        /// </summary>
        /// <param name="view">The console view for UI operations.</param>
        /// <param name="task4Controller">The controller of task 4.</param>
        public Task5Controller(ConsoleView view, ITaskController task4Controller)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._task4Controller = task4Controller ?? throw new ArgumentNullException(nameof(task4Controller));
        }

        /// <inheritdoc />
        public void RunTask()
        {
            this._view.PrintHeader("Task 5: Stack Trace Interpretation");
            try
            {
                this._view.WriteColored("Referenced from Task 4: ", ConsoleColor.Green);
                this._task4Controller.RunTask();
            }
            catch (Exception ex)
            {
                this._view.WriteColored("--- Stack Trace Begin ---\n", ConsoleColor.Magenta);
                this._view.WriteColored(ex.StackTrace ?? "No stack trace available.", ConsoleColor.DarkYellow);
                this._view.WriteColored("\n--- Stack Trace End ---\n\n", ConsoleColor.Magenta);

                this.PrintStackTraceInterpretation();
            }
        }

        private void PrintStackTraceInterpretation()
        {
            this._view.WriteColored("=== Stack Trace Interpretation ===\n", ConsoleColor.Cyan);

            this._view.WriteColored("1. Exception Type:\n", ConsoleColor.White);
            this._view.WriteColored("   - An unhandled runtime exception escaped from Task 4 execution.\n", ConsoleColor.Gray);
            this._view.WriteColored("   - The stack trace identifies the exact type of crash (e.g., NullReferenceException, KeyNotFoundException).\n\n", ConsoleColor.Gray);

            this._view.WriteColored("2. Root Cause Location (The Top of the Stack):\n", ConsoleColor.White);
            this._view.WriteColored("   - The VERY FIRST line of the stack trace block directly following 'at'.\n", ConsoleColor.Gray);
            this._view.WriteColored("   - This pinpointed source file name and line number show where execution broke.\n\n", ConsoleColor.Gray);

            this._view.WriteColored("3. Execution Flow (Bottom-to-Top Sequence):\n", ConsoleColor.White);
            this._view.WriteColored("   - The bottom lines show where the program execution started (e.g., Program.Main -> RunTask).\n", ConsoleColor.Gray);
            this._view.WriteColored("   - Each line above it represents a method calling another method, creating a active frame sequence.\n", ConsoleColor.Gray);
            this._view.WriteColored("   - The application bubble-up mechanism passed the failure context here to Task 5's catch block.\n\n", ConsoleColor.Gray);
            this._view.WriteColored("=============================================\n\n", ConsoleColor.Cyan);
        }
    }
}
