using Assignment5ExpenseTrackerEnhanced.Controller;
using Assignment5ExpenseTrackerEnhanced.IO;
using Assignment5ExpenseTrackerEnhanced.Persistence;
using Assignment5ExpenseTrackerEnhanced.Persistence.Csv;
using Assignment5ExpenseTrackerEnhanced.Services;
using Assignment5ExpenseTrackerEnhanced.Utilities;
using Assignment5ExpenseTrackerEnhanced.View;

namespace Assignment5ExpenseTrackerEnhanced
{
    /// <summary>
    /// Contains the Main entry point of the project.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Initializes dependencies and runs the controller.
        /// </summary>
        public static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8; // Used for rendering the Rupees symbol.

            // Serialization
            ITransactionCsvSerializer csvSerializer = new TransactionCsvSerializer();

            // Repository
            IRepository repository = new CsvFileRepository("transactions.csv", csvSerializer);

            // View and Utilities
            IConsoleIO consoleIo = new ConsoleIO();
            ConsoleHelper consoleHelper = new ConsoleHelper(consoleIo);
            IView view = new ConsoleView(consoleIo, consoleHelper);

            // Validation
            TransactionValidation validator = new TransactionValidation();

            // Service
            ITransactionService service = new TransactionService(repository, validator);

            // Controller
            ITransactionController controller = new TransactionController(service, view);

            // Application Runner
            ApplicationRunner applicationRunner = new ApplicationRunner(view, controller);

            applicationRunner.RunApplication();
        }
    }
}