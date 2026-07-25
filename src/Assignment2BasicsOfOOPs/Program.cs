namespace Assignment2BasicsOfOOPs
{
    using Assignment2BasicsOfOOPs.Controller;
    using Assignment2BasicsOfOOPs.IO;
    using Assignment2BasicsOfOOPs.Services;
    using Assignment2BasicsOfOOPs.Validation;
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Contains the Main entry point of the project.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Initializes dependencies and runs the controller.
        /// </summary>
        /// <param name="args">Optional CLI arguments.</param>
        internal static void Main(string[] args)
        {
            // View
            ConsoleIO consoleIo = new ConsoleIO();
            ConsoleView view = new ConsoleView(consoleIo);

            // Validation
            EmployeeValidation employeeValidator = new EmployeeValidation();
            BankValidation bankValidator = new BankValidation();
            ShapeValidation shapeValidator = new ShapeValidation();

            // Service
            ShapeService shapeService = new ShapeService();
            EmployeeService employeeService = new EmployeeService(employeeValidator);
            BankService bankService = new BankService(bankValidator);

            // Controller
            BankController bankController = new BankController(view, bankService);
            ShapeController shapeController = new ShapeController(view, shapeService, shapeValidator);
            EmployeeController employeeController = new EmployeeController(view, employeeService);

            MainController controller = new MainController(view, shapeController, employeeController, bankController);

            // Application Runner
            ApplicationRunner applicationRunner = new ApplicationRunner(view, controller);

            applicationRunner.RunApplication();
        }
    }
}