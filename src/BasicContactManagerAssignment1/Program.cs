namespace BasicContactManagerAssignment1
{
    using System;
    using BasicContactManagerAssignment1.IO;
    using BasicContactManagerAssignment1.Persistence;
    using BasicContactManagerAssignment1.Services;

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
            // Set up dependency injection chain (Composition Root)
            ConsoleIO consoleIo = new ConsoleIO();
            Repository repository = new Repository();
            ContactValidator validator = new ContactValidator();
            ContactManager contactManager = new ContactManager(repository, validator);
            ContactConsoleUI ui = new ContactConsoleUI(contactManager, consoleIo);

            // Execute the UI loop
            ui.Run();
        }
    }
}
