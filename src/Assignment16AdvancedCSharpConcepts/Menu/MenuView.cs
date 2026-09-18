using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Menu
{
    /// <summary>
    /// Represents the menu view for rendering UI.
    /// </summary>
    internal class MenuView
    {
        private const string AppName = "Assignment 16 - Advanced Concepts in C#";
        private const string GetChoicePrompt = "Enter your choice : ";

        private static readonly string[] Options = Enum.GetNames(typeof(MenuOptions));

        /// <summary>
        /// Displays the menu to the user.
        /// </summary>
        public void DisplayMenu()
        {
            ConsoleHelpers.WriteColored(AppName + "\n", ConsoleColor.Cyan);
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
        /// Pauses for the user input to return to the main menu.
        /// </summary>
        public void Pause()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
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
    }
}
