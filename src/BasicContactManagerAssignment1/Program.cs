namespace BasicContactManagerAssignment1
{
    using BasicContactManagerAssignment1.Controller;
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Persistence;
    using BasicContactManagerAssignment1.Services;
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
                ConsoleIO consoleIo = new ConsoleIO();
                Repository repository = new Repository();
                ContactValidator validator = new ContactValidator(repository);
                ContactManager contactManager = new ContactManager(repository, validator);
                ContactConsoleUI ui = new ContactConsoleUI(consoleIo);
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