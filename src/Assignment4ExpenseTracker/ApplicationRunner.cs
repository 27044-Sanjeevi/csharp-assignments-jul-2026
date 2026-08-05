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
        private const int MaxMenuChoice = 7;

        private readonly IView _view;
        private readonly IFinanceController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRunner"/> class.
        /// </summary>
        /// <param name="view">The console view for displaying information to the user.</param>
        /// <param name="controller">The main controller responsible for managing application logic.</param>
        public ApplicationRunner(IView view, IFinanceController controller)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        /// <summary>
        /// Runs the main application loop, displaying the main menu and handling user choices until exit is requested.
        /// </summary>
        public void RunApplication()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    this._view.ClearScreen();
                    this._view.ShowMainMenu();
                    int choice = this._view.ReadChoice(MinMenuChoice, MaxMenuChoice);
                    this._view.ClearScreen();

                    exit = this._controller.HandleTransactionMenu(choice);

                    if (!exit)
                    {
                        this._view.PauseAndReturn();
                    }
                }
                catch (ArgumentException ex)
                {
                    this._view.DisplayError(ex.Message);
                    this._view.PauseAndReturn();
                }
                catch (KeyNotFoundException ex)
                {
                    this._view.DisplayError(ex.Message);
                    this._view.PauseAndReturn();
                }
                catch (Exception ex)
                {
                    this._view.DisplayError($"Unexpected error: {ex.Message}");
                    this._view.PauseAndReturn();
                }
            }
        }
    }
}
