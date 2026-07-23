namespace Assignment2BasicsOfOOPs
{
    using System;
    using Assignment2BasicsOfOOPs.Controller;
    using Assignment2BasicsOfOOPs.Services;
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
            ConsoleView view = new ConsoleView();

            ShapeService shapeService = new ShapeService();
            EmployeeService employeeService = new EmployeeService();
            BankService bankService = new BankService();

            BankController bankController = new BankController(view, bankService);
            ShapeController shapeController = new ShapeController(view, shapeService);
            EmployeeController employeeController = new EmployeeController(view, employeeService);

            MainController controller = new MainController(view, shapeController, employeeController, bankController);

            controller.RunApplication();
        }
    }
}