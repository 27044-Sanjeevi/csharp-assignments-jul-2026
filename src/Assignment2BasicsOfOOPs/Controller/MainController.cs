namespace Assignment2BasicsOfOOPs.Controller
{
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Coordinates operations between the UI/View and the Service layer.
    /// </summary>
    internal class MainController
    {
        private const int MinTaskChoice = 1;
        private const int MaxTaskChoice = 4;

        private readonly ConsoleView _view;

        private ShapeController _shapeController;
        private EmployeeController _employeeController;
        private BankController _bankController;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="shapeController">The Controller layer for Shape.</param>
        /// <param name="employeeController">The controller layer for the Employee paroll.</param>
        /// <param name="bankController">The controller layer for the bank services</param>
        public MainController(ConsoleView view, ShapeController shapeController, EmployeeController employeeController, BankController bankController)
        {
            this._view = view;
            this._shapeController = shapeController;
            this._employeeController = employeeController;
            this._bankController = bankController;
        }

        /// <summary>
        /// Runs the main application control loop.
        /// </summary>
        public void RunApplication()
        {
            int choice = 0;
            do
            {
                this._view.ClearScreen();
                this._view.ShowMainMenu();
                choice = this._view.ReadChoice(MinTaskChoice, MaxTaskChoice);
                this._view.ClearScreen();

                switch (choice)
                {
                    case 1:
                        this._shapeController.RunShapeTask();
                        break;
                    case 2:
                        this._employeeController.RunEmployeeTask();
                        break;
                    case 3:
                        this._bankController.RunBankTask();
                        break;
                    case 4:
                        this._view.PrintGoodbye();
                        break;
                }

                if (choice != 4)
                {
                    this._view.PauseAndReturn();
                }
            }
            while (choice != 4);
        }
    }
}
