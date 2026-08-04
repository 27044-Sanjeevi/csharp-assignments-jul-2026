namespace Assignments
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
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                // Repository
                IRepository repository = new InMemoryRepository();

                // View and Utilities
                IIo consoleIo = new ConsoleIO();
                ConsoleHelper consoleHelper = new ConsoleHelper(consoleIo);
                ConsoleView view = new ConsoleView(consoleIo, consoleHelper);

                // Validation
                ITransactionValidation validator = new TransactionValidation();

                // Service
                ITransactionService service = new TransactionService(repository, validator);

                // Controller
                FinanceController controller = new FinanceController(service, view);

                // Application Runner
                ApplicationRunner applicationRunner = new ApplicationRunner(view, controller);

                applicationRunner.RunApplication();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}