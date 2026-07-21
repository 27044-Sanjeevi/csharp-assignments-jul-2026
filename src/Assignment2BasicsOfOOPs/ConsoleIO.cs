namespace Assignment2BasicsOfOOPs
{
    /// <summary>
    /// contains static methods for console IO operations
    /// </summary>
    internal class ConsoleIO
    {
        /// <summary>
        /// writes the message in the color with the given console color
        /// </summary>
        /// <param name="message">message to be printed</param>
        /// <param name="consoleColor">color of the message to be displayed</param>
        public static void WriteColored(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// writes the message in the console
        /// </summary>
        /// <param name="message">message to be displayed</param>
        public static void Write(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Prompts the user continuously until they enter any valid integer.
        /// </summary>
        /// <param name="prompt">Instruction message displayed to the user.</param>
        /// <returns>The integer parsed from the console line</returns>
        public static int ReadInt(string prompt)
        {
            int result;

            Console.Write(prompt);

            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Clear();
                ConsoleIO.WriteColored("[INPUT ERROR] Invalid integer. Please enter numeric characters only.", ConsoleColor.Red);
                Console.ResetColor();
                ConsoleIO.Write("\n");
                ConsoleIO.Write(prompt);
            }

            return result;
        }
    }
}
