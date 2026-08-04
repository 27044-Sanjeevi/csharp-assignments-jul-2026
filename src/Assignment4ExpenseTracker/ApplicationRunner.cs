namespace Assignment4ExpenseTracker
{
    using Assignment4ExpenseTracker;
    using Assignment4ExpenseTracker.Controller;
    using Assignment4ExpenseTracker.IO;
    using Assignment4ExpenseTracker.Persistence;
    using Assignment4ExpenseTracker.Services;
    using Assignment4ExpenseTracker.Services.Validation;
    using Assignment4ExpenseTracker.Utilities;
    using Assignment4ExpenseTracker.View;

    /// <summary>
    /// Coordinates the main application workflow.
    /// </summary>
    internal class ApplicationRunner
    {
        private const int MinMenuChoice = 1;
        private const int MaxMenuChoice = 5;

        private readonly ConsoleView _view;
        private readonly FinanceController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRunner"/> class.
        /// </summary>
        /// <param name="view">The console view for displaying information to the user.</param>
        /// <param name="controller">The main controller responsible for managing application logic.</param>
        public ApplicationRunner(ConsoleView view, FinanceController controller)
        {
            this._view = view ?? throw new ArgumentException(nameof(view));
            this._controller = controller ?? throw new ArgumentException(nameof(controller));
        }

        /// <summary>
        /// Runs the main application loop, displaying the main menu and handling user choices until exit is requested.
        /// </summary>
        public void RunApplication()
        {
            bool exit = false;
            try
            {
                while (!exit)
                {
                    this._view.ClearScreen();

                    int choice = this._view.ReadChoice(MinMenuChoice, MaxMenuChoice);

                    this._view.ClearScreen();
                    try
                    {
                        exit = this._controller.HandleTransactionMenu(choice);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    if (!exit)
                    {
                        this._view.PauseAndReturn();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                this._view.DisplayError(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                this._view.DisplayError(ex.Message);
            }
            catch (Exception ex)
            {
                this._view.DisplayError(ex.Message);
            }
        }
    }
}
