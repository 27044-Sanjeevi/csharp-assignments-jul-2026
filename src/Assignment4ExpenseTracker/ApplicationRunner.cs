namespace Assignment4ExpenseTracker
{
    using Assignment4ExpenseTracker.Controller;
    using Assignment4ExpenseTracker.View;

    /// <summary>
    /// Coordinates the main application workflow.
    /// </summary>
    internal class ApplicationRunner
    {
        private const int MinMenuChoice = 1;
        private const int MaxMenuChoice = 7;

        private readonly IView _view;
        private readonly ITransactionController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRunner"/> class.
        /// </summary>
        /// <param name="view">The console view for displaying information to the user.</param>
        /// <param name="controller">The main controller responsible for managing application logic.</param>
        public ApplicationRunner(IView view, ITransactionController controller)
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

                    exit = this._controller.HandleMenu(choice);

                    if (!exit)
                    {
                        this._view.PauseAndReturn();
                    }
                }
                catch (ArgumentException ex)
                {
                    this._view.HandleError(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    this._view.HandleError(ex.Message);
                }
                catch (Exception ex)
                {
                    this._view.HandleError($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}
