using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9LINQAdvanced
{
    /// <summary>
    /// Contains the helper methods for console operations.
    /// </summary>
    internal static class ConsoleHelpers
    {
        /// <summary>
        /// Prompts the user continuously until they enter a valid choice in the specified range.
        /// </summary>
        /// <param name="max">The maximum valid choice.</param>
        /// <param name="message">Optional message to be displayed.</param>
        /// <returns>A valid choice integer.</returns>
        public static int ReadChoice(int max, string? message = null)
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
        /// Pauses until user presses any key.
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("Press any key to return to main menu..");
            Console.ReadKey();
        }
    }
}
