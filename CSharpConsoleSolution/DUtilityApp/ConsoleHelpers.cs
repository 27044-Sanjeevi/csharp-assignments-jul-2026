namespace DUtilityApp
{
    /// <summary>
    /// Represents the helpers for console operations.
    /// </summary>
    public static class ConsoleHelpers
    {
        /// <summary>
        /// Displays the given title in a titled format.
        /// </summary>
        /// <param name="title">The title to be displayed.</param>
        public static void DisplayTitle(string title)
        {
            WriteColored($"----- {title} -----\n", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Displays the message in subtitled format.
        /// </summary>
        /// <param name="subtitle">Subtitle to be displayed.</param>
        public static void DisplaySubtitle(string subtitle)
        {
            WriteColored($"> {subtitle}\n", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Displays the given status message.
        /// </summary>
        /// <param name="message">The status message to be displayed.</param>
        public static void DisplayStatus(string message)
        {
            WriteColored($"\n{message}...\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Displays the given failure message.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        public static void DisplayFailure(string message)
        {
            WriteColored($"{message}\n", ConsoleColor.Red);
        }

        /// <summary>
        /// Displays the given success message.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        public static void DisplaySuccess(string message)
        {
            WriteColored($"{message}\n", ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Prints a line into the console.
        /// </summary>
        public static void PrintLine()
        {
            Console.WriteLine("\n" + new string('-', 40));
        }

        /// <summary>
        /// Writes the message as a colored text.
        /// </summary>
        /// <param name="message">Message to be written.</param>
        /// <param name="color">Color of the text message.</param>
        public static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed integer value.</returns>
        public static int ReadInteger(string prompt)
        {
            while (true)
            {
                string? input = ReadLine(prompt);
                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                WriteColored("[INPUT ERROR] Invalid number. Please enter a valid integer value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Reads a string value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The trimmed string input.</returns>
        public static string ReadString(string prompt)
        {
            while (true)
            {
                string? input = ReadLine(prompt);

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                WriteColored("[INPUT ERROR] Input cannot be empty. Please try again.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Reads a decimal value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed decimal value.</returns>
        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                string? input = ReadLine(prompt);

                if (decimal.TryParse(input, out decimal value))
                {
                    return value;
                }

                WriteColored("[INPUT ERROR] Please enter a valid decimal value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Reads a decimal value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed character.</returns>
        public static char ReadChar(string prompt)
        {
            while (true)
            {
                string? input = ReadLine(prompt);

                if (char.TryParse(input, out char value) && value >= 0.0M)
                {
                    return value;
                }

                WriteColored("[INPUT ERROR] Please enter a valid character.\n", ConsoleColor.Red);
            }
        }

        private static string? ReadLine(string? prompt = "")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
