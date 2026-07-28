namespace Assignment3InventoryManagement
{
    using Assignment3InventoryManagement.Controller;
    using Assignment3InventoryManagement.View;

    /// <summary>
    /// Coordinates the main application workflow.
    /// </summary>
    internal class ApplicationRunner
    {
        private const int MinTaskChoice = 1;
        private const int MaxTaskChoice = 9;

        private readonly ConsoleView _view;
        private readonly MainController _mainController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRunner"/> class.
        /// </summary>
        /// <param name="view">The console view for displaying information to the user.</param>
        /// <param name="mainController">The main controller responsible for managing application logic.</param>
        public ApplicationRunner(ConsoleView view, MainController mainController)
        {
            this._view = view ?? throw new ArgumentException(nameof(ConsoleView));
            this._mainController = mainController ?? throw new ArgumentException(nameof(MainController));
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
                    this._view.ShowMainMenu();

                    int choice = this._view.ReadChoice(MinTaskChoice, MaxTaskChoice);

                    this._view.ClearScreen();

                    exit = this._mainController.HandleMenu(choice);

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
