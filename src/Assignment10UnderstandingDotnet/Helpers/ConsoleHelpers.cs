namespace Assignment10UnderstandingDotnet.Utilities
{
    /// <summary>
    /// Provides the methods for console related helper operations.
    /// </summary>
    internal static class ConsoleHelpers
    {
        /// <summary>
        /// Reads an integer value from the console.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed integer value.</returns>
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        /// <summary>
        /// Writes the given message in the given console color.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        /// <param name="color">Color of the message to be displayed in.</param>
        public static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an exception message in red color.
        /// </summary>
        /// <param name="message">The exception message to be displayed.</param>
        public static void DisplayException(string message)
        {
            WriteColored($"{message}", ConsoleColor.Red);
        }
    }
}
