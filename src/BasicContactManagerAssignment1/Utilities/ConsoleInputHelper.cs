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
        /// Prompts the user for input and returns the input if it is not null or whitespace.
        /// </summary>
        /// <param name="prompt">The message displayed to the user to request input.</param>
        /// <returns>The user input as a string, or null if the input is null or whitespace.</returns>
        public string? TryGetRequiredString(string prompt)
        {
            this._consoleIo.Write(prompt);
            string? input = this._consoleIo.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        /// <summary>
        /// Prompts for a menu choice and returns the validated selection within the specified range.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        /// <param name="min">The minimum valid menu choice.</param>
        /// <param name="max">The maximum valid menu choice.</param>
        /// <returns>The selected menu choice if valid; otherwise, -1.</returns>
        public int TryGetMenuChoice(string prompt, int min, int max)
        {
            this._consoleIo.Write(prompt);
            string? inpu = this._consoleIo.ReadLine();
            if (int.TryParse(inpu, out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }

            return -1; // value for invalid
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
                string? cleanInput = this.ReadCleanLine(prompt);
                if (cleanInput != null)
                {
                    return cleanInput;
                }

                this._consoleIo.WriteLine("[INPUT ERROR] Value cannot be empty.");
            }
        }

        /// <summary>
        /// Prompts the user for an optional string input (allows empty or whitespace).
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The input string, trimmed, or null if empty.</returns>
        public string? GetOptionalString(string prompt)
        {
            return this.ReadCleanLine(prompt);
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
                string? rawInput = this.ReadCleanLine(prompt);

                if (int.TryParse(rawInput, out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                this._consoleIo.WriteColored($"[INPUT ERROR] Please enter a valid number between {min} and {max}.", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// A centralized method to ensure basic trimming.
        /// </summary>
        private string? ReadCleanLine(string prompt)
        {
            this._consoleIo.Write(prompt);
            string? input = this._consoleIo.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }
    }
}
