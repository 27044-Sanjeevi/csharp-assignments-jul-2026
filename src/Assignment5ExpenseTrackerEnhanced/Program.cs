namespace Assignment5ExpenseTrackerEnhanced
{
    using Assignment5ExpenseTrackerEnhanced.Controller;
    using Assignment5ExpenseTrackerEnhanced.IO;
    using Assignment5ExpenseTrackerEnhanced.Persistence;
    using Assignment5ExpenseTrackerEnhanced.Services;
    using Assignment5ExpenseTrackerEnhanced.Services.Validation;
    using Assignment5ExpenseTrackerEnhanced.Utilities;
    using Assignment5ExpenseTrackerEnhanced.View;

    /// <summary>
    /// Contains the Main entry point of the project.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Initializes dependencies and runs the controller.
        /// </summary>
        /// <param name="args">Optional CLI arguments.</param>
        public static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8; // Used for rendering the Rupees symbol.

            // Repository
            IRepository repository = new InMemoryRepository();

            // View and Utilities
            IIo consoleIo = new ConsoleIO();
            ConsoleHelper consoleHelper = new ConsoleHelper(consoleIo);
            IView view = new ConsoleView(consoleIo, consoleHelper);

            // Validation
            ITransactionValidation validator = new TransactionValidation();

            // Service
            ITransactionService service = new TransactionService(repository, validator);

            // Controller
            IFinanceController controller = new FinanceController(service, view);

            // Application Runner
            ApplicationRunner applicationRunner = new ApplicationRunner(view, controller);

            applicationRunner.RunApplication();
        }
    }
}