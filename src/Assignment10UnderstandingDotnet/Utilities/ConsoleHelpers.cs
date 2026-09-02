namespace Assignment10UnderstandingDotnet.Utilities
{
    /// <summary>
    /// Provides the methods for console realted helper operations.
    /// </summary>
    internal class ConsoleHelpers
    {
        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The parsed integer value, or null if the field was skipped.</returns>
        public static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
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
            WriteColored($"[EXCEPTION] {message}", ConsoleColor.Red);
        }
    }
}
