using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment8ErrorHandling.IO;

namespace Assignment8ErrorHandling.Utilities
{
    /// <summary>
    /// Provides the methods for console realted helper operations.
    /// </summary>
    internal class ConsoleHelper
    {
        private readonly IConsoleIO _consoleIO;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHelper"/> class.
        /// </summary>
        /// <param name="consoleIO">The console input and output renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object passed is null.</exception>
        internal ConsoleHelper(IConsoleIO consoleIO)
        {
            this._consoleIO = consoleIO ?? throw new ArgumentNullException(nameof(consoleIO));
        }

        public int? ReadPositiveInt(string prompt, bool isOptional = false)
        {
            const int MaxAttempts = 3;
            int attempt = 0;

            while (attempt < MaxAttempts)
            {
                string? input = this._consoleIO.ReadLine(prompt);

                if (isOptional && string.IsNullOrEmpty(input))
                {
                    return null;
                }

                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }

                attempt++;
                if (attempt < MaxAttempts)
                {
                    this.DisplayWarning($"Please enter a Positive Integer Value ({attempt}/{MaxAttempts} attempts remaining).");
                }
            }

            this.AbortInputOperation();
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



        public void AbortInputOperation()
        {
            this.DisplayError("Correct Value is not entered, aborting input operation.");
        }
    }
}
