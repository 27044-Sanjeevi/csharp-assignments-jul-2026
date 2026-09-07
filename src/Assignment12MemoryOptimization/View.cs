using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12MemoryOptimization
{
    internal class View
    {
        private const string AppName = "Assignment 11 - Memory Optimization Techniques";
        private const string GetChoicePrompt = "Enter your choice : ";

        // used array instead of an enum for simplicity.
        private static readonly string[] Options = Enum.GetNames(typeof(MenuOptions));

        public void DisplayMenu()
        {
            this.WriteColored(AppName + "\n", ConsoleColor.Cyan);
            for (int i = 0; i < Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {this.ToHumanFriendlyString(Options[i])}");
            }
        }

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

        private void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
