namespace BasicContactManagerAssignment1
{
    using BasicContactManagerAssignment1.Controller;
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Persistence;
    using BasicContactManagerAssignment1.Services;
    using BasicContactManagerAssignment1.Utilities;
    using BasicContactManagerAssignment1.Validation;
    using BasicContactManagerAssignment1.View;

    /// <summary>
    /// Provides the main entry point for the contact manager console application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            try
            {
                // Persistence
                Repository repository = new Repository();

                // Services
                ContactValidator validator = new ContactValidator();
                ContactManager contactManager = new ContactManager(repository, validator);

                // UI
                ConsoleIO consoleIo = new ConsoleIO();
                ContactConsoleUI ui = new ContactConsoleUI(consoleIo, helpers);

                // Helper
                ConsoleInputHelper helpers = new ConsoleInputHelper(consoleIo);

                // Controller
                ContactController controller = new ContactController(contactManager, ui);

                // Execute the UI loop
                controller.HandleMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}