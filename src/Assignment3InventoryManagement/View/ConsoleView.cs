using Assignment3InventoryManagement.IO;

namespace Assignment3InventoryManagement.View
{
    /// <summary>
    /// Handles presentation rendering, headers, menus, and user inputs for the console UI.
    /// </summary>
    internal class ConsoleView
    {
        private readonly ConsoleIO _consoleIo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleIo">The console view input output renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the Argument is null.</exception>
        public ConsoleView(ConsoleIO consoleIo)
        {
            this._consoleIo = new ConsoleIO() ?? throw new ArgumentNullException(nameof(consoleIo));
        }

        /// <summary>
        /// Displays the main application task menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this.WriteColored(
                "Available Operations:\n\n" +
                "1. Add a Product\n" +
                "2. View all Products\n" +
                "3. Search Product\n" +
                "4. Update a Product" +
                "5. Delete Product" +
                "6. Add stock" +
                "7. Remove stock" +
                "8. Exit Application\n\n",
                ConsoleColor.Cyan);
            this.Write("Choose the Task to run: ");
        }

        /// <summary>
        /// Writes message in a custom console color.
        /// </summary>
        /// <param name="message">The text to write.</param>
        /// <param name="color">The target console color.</param>
        public void WriteColored(string message, ConsoleColor color)
        {
            this._consoleIo.WriteColored(message, color);
        }

        /// <summary>
        /// Displays the message given.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void Write(string message)
        {
            this._consoleIo.Write(message);
        }

        /// <summary>
        /// Writes the message with a new line.
        /// </summary>
        /// <param name="message">Message to be written on console.</param>
        public void WriteLine(string message)
        {
            this._consoleIo.WriteLine(message);
        }

        /// <summary>
        /// Reads a the input from the user as string.
        /// </summary>
        /// <param name="prompt">Optional prompt to be displayed.</param>
        /// <returns>The read string value.</returns>
        public string? ReadLine(string? prompt = "")
        {
            return this._consoleIo.ReadLine(prompt);
        }
    }
}
