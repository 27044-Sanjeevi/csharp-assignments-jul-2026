using Assignment3InventoryManagement;
using Assignment3InventoryManagement.Controller;
using Assignment3InventoryManagement.IO;
using Assignment3InventoryManagement.Persistence;
using Assignment3InventoryManagement.Services;
using Assignment3InventoryManagement.Validation;
using Assignment3InventoryManagement.View;

namespace Assignments
{
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
            // Repository
            Repository repository = new Repository();

            // View
            ConsoleIO consoleIo = new ConsoleIO();
            ConsoleView view = new ConsoleView(consoleIo);

            // Validation
            ProductValidation productValidator = new ProductValidation();

            // Service
            InventoryService inventoryService = new InventoryService(repository, productValidator);

            // Controller
            MainController mainController = new MainController(view, inventoryService);

            // Application Runner
            ApplicationRunner applicationRunner = new ApplicationRunner(view, mainController);

            applicationRunner.RunApplication();
        }
    }
}