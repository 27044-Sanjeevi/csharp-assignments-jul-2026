using System.Text;

namespace Assignment12MemoryOptimization
{
    /// <summary>
    /// Represents the console view for rendering UI.
    /// </summary>
    internal class View
    {
        private const string AppName = "Assignment 12 - Memory Optimization Techniques";
        private const string GetChoicePrompt = "Enter your choice : ";

        // used array instead of an enum for simplicity.
        private static readonly string[] Options = Enum.GetNames(typeof(MenuOptions));

        /// <summary>
        /// Displays the menu to the user.
        /// </summary>
        public void DisplayMenu()
        {
            this.WriteColored(AppName + "\n", ConsoleColor.Cyan);
            for (int i = 0; i < Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {this.ToHumanFriendlyString(Options[i])}");
            }
        }

        /// <summary>
        /// Retrieves the menu choice from the user.
        /// </summary>
        /// <returns>An enum specifying the menu option.</returns>
        public MenuOptions GetMenuChoice()
        {
            return (MenuOptions)this.ReadChoice(Options.Length, "\n" + GetChoicePrompt);
        }

        /// <summary>
        /// Prompts the user continuously until they enter a valid choice in the specified range.
        /// </summary>
        /// <param name="max">The maximum valid choice.</param>
        /// <param name="message">Optional message to be displayed.</param>
        /// <returns>A valid choice integer.</returns>
        public int ReadChoice(int max, string? message = null)
        {
            int result;

            if (message != null)
            {
                Console.Write(message);
            }

            while (!int.TryParse(Console.ReadLine(), out result) || result < 1 || result > max)
            {
                Console.Write($"[INPUT ERROR] Invalid Choice. Choose an integer between 1 to {max}: ");
            }

            return result;
        }

        /// <summary>
        /// Displays the exit instruction to the user.
        /// </summary>
        public void DisplayExitInstruction()
        {
            this.WriteColored("\nNOTE: Press Ctrl + C to stop the application after choosing a task to stop the infinite loop.", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Pauses for the user input to return to the main menu.
        /// </summary>
        public void Pause()
        {
            Console.WriteLine("\nPress any key to exit to the main menu...");
            Console.ReadKey();
        }

        /// <summary>
        /// Converts the given text in Pascal Case to human-friendly spaced string.
        /// </summary>
        /// <param name="text">The text in Pascal case.</param>
        /// <returns>A human-friendly spaced string.</returns>
        private string ToHumanFriendlyString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            builder.Append(text[0]); // Add the very first character directly

            for (int i = 1; i < text.Length; i++)
            {
                // If the character is uppercase, add a space before appending it
                if (char.IsUpper(text[i]))
                {
                    builder.Append(' ');
                }

                builder.Append(text[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Writes the given message in the given console color.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="color">The color of the message.</param>
        private void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
