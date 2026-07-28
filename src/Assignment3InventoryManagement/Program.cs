namespace Assignments
{
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;
    using Assignment3InventoryManagement;
    using Assignment3InventoryManagement.Controller;
    using Assignment3InventoryManagement.IO;
    using Assignment3InventoryManagement.Persistence;
    using Assignment3InventoryManagement.Services;
    using Assignment3InventoryManagement.Utilities;
    using Assignment3InventoryManagement.Validation;
    using Assignment3InventoryManagement.View;

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
            try
            {
                // Repository
                IRepository repository = new Repository();

                // View and Utilities
                IConsoleIO consoleIo = new ConsoleIO();
                IConsoleHelper consoleHelper = new ConsoleHelper(consoleIo);
                ConsoleView view = new ConsoleView(consoleHelper);

                // Validation
                IProductValidation productValidator = new ProductValidation();

                // Service
                IInventoryService inventoryService = new InventoryService(repository, productValidator);

                // Controller
                MainController mainController = new MainController(view, inventoryService);

                // Application Runner
                ApplicationRunner applicationRunner = new ApplicationRunner(view, mainController);

                applicationRunner.RunApplication();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}