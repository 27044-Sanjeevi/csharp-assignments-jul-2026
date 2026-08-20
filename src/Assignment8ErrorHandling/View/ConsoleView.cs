namespace Assignment8ErrorHandling.View
{
    using System;
    using Assignment8ErrorHandling.IO;
    using Assignment8ErrorHandling.Utilities;

    /// <summary>
    /// Handles presentation rendering, headers, menus, and user inputs for the console UI.
    /// </summary>
    internal class ConsoleView
    {
        private readonly ConsoleIO _consoleIo;
        private readonly ConsoleHelper _consoleHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleIo">The console view input output renderer.</param>
        /// <param name="consoleHelper">The console helper for generic input, output, and formatting operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the Argument is null.</exception>
        public ConsoleView(ConsoleIO consoleIo, ConsoleHelper consoleHelper)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
            this._consoleHelper = consoleHelper ?? throw new ArgumentNullException(nameof(consoleHelper));
        }

        /// <summary>
        /// Displays the main application task menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this.WriteColored(
                "Available Tasks:\n" +
                "1. Task 1 - try/catch/finally (Division by Zero)\n" +
                "2. Task 2 - Catching and Throwing (Index Out of Range)\n" +
                "3. Task 3 - Custom Exception Class (User Input Verification)\n" +
                "4. Task 4 - Global Unhandled Exception Handling\n" +
                "5. Task 5 - Nesting and Stack Trace Analysis\n" +
                "6. Exit Application\n\n",
                ConsoleColor.Cyan);
            this.Write("Choose the Task to run: ");
        }

        /// <summary>
        /// Prompts the user continuously until they enter a valid choice in the specified range.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <param name="message">Optional message to be displayed.</param>
        /// <returns>A valid choice integer.</returns>
        public int ReadChoice(int min, int max, string? message = null)
        {
            int result;

            if (message != null)
            {
                this.Write(message);
            }

            while (!int.TryParse(this.ReadLine(), out result) || result < min || result > max)
            {
                this.WriteColored($"[INPUT ERROR] Invalid Choice. Choose an integer between {min} to {max}: ", ConsoleColor.Red);
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for a non-empty string.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The validated string input.</returns>
        public string ReadString(string prompt)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                this.WriteColored("[INPUT ERROR] Input cannot be empty. Please try again.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void ClearScreen()
        {
            this._consoleIo.Clear();
        }

        /// <summary>
        /// Prints a colored task header.
        /// </summary>
        /// <param name="title">The task title.</param>
        public void PrintHeader(string title)
        {
            this.WriteColored($"=== {title} ===\n\n", ConsoleColor.Blue);
        }

        /// <summary>
        /// Prints a colored sub-header.
        /// </summary>
        /// <param name="text">The sub-header text.</param>
        public void PrintSubHeader(string text)
        {
            this.WriteColored($"{text}\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Prints a divider line.
        /// </summary>
        public void PrintDivider()
        {
            this.Write("\n" + new string('-', 40) + "\n\n");
        }

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        public void PauseAndReturn()
        {
            this.Write("\nPress any key to return to Main Page...");
            this._consoleIo.ReadKey(true);
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
        /// Reads the input from the user as string.
        /// </summary>
        /// <param name="prompt">Optional prompt to be displayed.</param>
        /// <returns>The read string value.</returns>
        public string? ReadLine(string? prompt = "")
        {
            return this._consoleIo.ReadLine(prompt);
        }

        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt to be displayed.</param>
        /// <returns>The parsed integer from the user.</returns>
        public int ReadInt(string prompt)
        {
            return this._consoleHelper.ReadInt(prompt);
        }
    }
}
