namespace BasicContactManagerAssignment1.Utilities
{
    using System;
    using BasicContactManagerAssignment1.IO;

    /// <summary>
    /// Provides helper methods for reading and validating console input.
    /// </summary>
    internal class ConsoleInputHelper
    {
        private readonly ConsoleIO _consoleIo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleInputHelper"/> class.
        /// </summary>
        /// <param name="consoleIo">The console I/O wrapper.</param>
        /// <exception cref="ArgumentNullException">Thrown when consoleIo is null.</exception>
        public ConsoleInputHelper(ConsoleIO consoleIo)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
        }

        /// <summary>
        /// Prompts the user for a non-empty string input.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>A validated non-empty string.</returns>
        public string GetRequiredString(string prompt)
        {
            while (true)
            {
                this._consoleIo.Write(prompt);
                string? input = this._consoleIo.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                this._consoleIo.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user for an optional string input (allows empty or whitespace).
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The input string, trimmed, or null if empty.</returns>
        public string? GetOptionalString(string prompt)
        {
            this._consoleIo.Write(prompt);
            string? input = this._consoleIo.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            return input.Trim();
        }

        /// <summary>
        /// Prompts the user for a valid menu choice within the specified range.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <returns>A valid integer choice.</returns>
        public int GetMenuChoice(string prompt, int min, int max)
        {
            while (true)
            {
                this._consoleIo.Write(prompt);
                string? input = this._consoleIo.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                this._consoleIo.WriteLine($"Invalid choice. Please enter a number between {min} and {max}.");
            }
        }

        /// <summary>
        /// Prompts the user with a yes/no question.
        /// </summary>
        /// <param name="prompt">The prompt question to display.</param>
        /// <returns>True if the user chose yes; false if they chose no.</returns>
        public bool GetConfirmation(string prompt)
        {
            while (true)
            {
                this._consoleIo.Write($"{prompt} (y/n): ");
                string? input = this._consoleIo.ReadLine()?.Trim().ToLowerInvariant();

                if (input == "y" || input == "yes")
                {
                    return true;
                }

                if (input == "n" || input == "no")
                {
                    return false;
                }

                this._consoleIo.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        /// <summary>
        /// Truncates a string to the specified maximum length, appending "..." if it exceeds that length.
        /// </summary>
        /// <param name="value">String to be truncated.</param>
        /// <param name="maxLength">Maxlength allowed to be printed</param>
        /// <returns>Truncated message</returns>
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) ||
                value.Length <= maxLength)
            {
                return value;
            }

            return value.Substring(0, maxLength - 3) + "...";
        }
    }
}
