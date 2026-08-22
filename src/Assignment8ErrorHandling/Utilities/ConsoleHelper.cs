namespace Assignment8ErrorHandling.Utilities
{
    using Assignment8ErrorHandling.IO;

    /// <summary>
    /// Provides the methods for console realted helper operations.
    /// </summary>
    internal class ConsoleHelper
    {
        private readonly ConsoleIO _consoleIO;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHelper"/> class.
        /// </summary>
        /// <param name="consoleIO">The console input and output renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object passed is null.</exception>
        internal ConsoleHelper(ConsoleIO consoleIO)
        {
            this._consoleIO = consoleIO ?? throw new ArgumentNullException(nameof(consoleIO));
        }

        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed integer value, or null if the field was skipped.</returns>
        public int ReadInt(string prompt)
        {
            const int MaxAttempts = 3;
            int attempt = 0;

            while (attempt < MaxAttempts)
            {
                string? input = this._consoleIO.ReadLine(prompt);
                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                attempt++;
                if (attempt < MaxAttempts)
                {
                    this.DisplayWarning($"Please enter an integer value ({attempt}/{MaxAttempts} attempts remaining).");
                }
            }

            this.AbortInputOperation();
            return 0;
        }

        /// <summary>
        /// Displays the given text in the header format.
        /// </summary>
        /// <param name="header">The header to be displayed.</param>
        public void PrintHeader(string header)
        {
            this._consoleIO.WriteColored($"========= {header} =========\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Displays a warning message to the user.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public void DisplayWarning(string message)
        {
            this._consoleIO.WriteColored($"[WARNING] {message}\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Aborts the input operation.
        /// </summary>
        public void AbortInputOperation()
        {
            this._consoleIO.WriteColored("Correct Value is not entered, aborting input operation.", ConsoleColor.Red);
        }
    }
}
