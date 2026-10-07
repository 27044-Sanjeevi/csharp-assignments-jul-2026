namespace Assignment17Reflection.Utilities
{
    /// <summary>
    /// Represents the helper for console operations.
    /// </summary>
    internal static class ConsoleHelpers
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
        /// Writes the message as a colored text with a newline.
        /// </summary>
        /// <param name="message">Message to be written.</param>
        /// <param name="color">Color of the text message.</param>
        public static void WriteLineColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
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
        /// Prompts the user for a non-empty string.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The validated string input.</returns>
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
        /// Reads the input from the user as string.
        /// </summary>
        /// <param name="prompt">Optional prompt to be displayed.</param>
        /// <returns>The read string value.</returns>
        private static string? ReadLine(string? prompt = "")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
