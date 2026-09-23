namespace Assignment16AdvancedCSharpConcepts
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
        /// <param name="subtitle">The subtitle text to be displayed.</param>
        public static void DisplaySubtitle(string subtitle)
        {
            WriteColored($"\n> {subtitle}\n", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Displays the given status message.
        /// </summary>
        /// <param name="message">The status message to be displayed.</param>
        public static void DisplayStatus(string message)
        {
            WriteColored($"\n{message}\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Displays the success message.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public static void DisplaySuccessMessage(string message)
        {
            WriteColored(message + Environment.NewLine, ConsoleColor.Green);
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
        /// Prints the given code snippet with newlines.
        /// </summary>
        /// <param name="codeSnippet">The code snippet to be printed.</param>
        public static void DisplayCodeSnippet(string codeSnippet)
        {
            Console.WriteLine(Environment.NewLine + codeSnippet + Environment.NewLine);
        }

        /// <summary>
        /// Prints a line into the console.
        /// </summary>
        public static void DisplayLine()
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
    }
}
