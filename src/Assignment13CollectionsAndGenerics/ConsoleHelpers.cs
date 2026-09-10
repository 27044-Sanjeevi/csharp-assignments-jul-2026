namespace Assignment13CollectionsAndGenerics
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
        /// Displays the given status message.
        /// </summary>
        /// <param name="message">The status message to be displayed.</param>
        public static void DisplayStatus(string message)
        {
            WriteColored($"\n{message}\n", ConsoleColor.Yellow);
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
