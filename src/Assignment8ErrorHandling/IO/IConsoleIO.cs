using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment8ErrorHandling.IO
{
    /// <summary>
    /// Specifies the contract for Console Input and Output operations.
    /// </summary>
    internal interface IConsoleIO
    {

        /// <summary>
        /// Prompts an optional message and reads a string from the user.
        /// </summary>
        /// <param name="prompt">The optional message to display to the user.</param>
        /// <returns>The string read from the user.</returns>
        string? ReadLine(string? prompt);

        /// <summary>
        /// Writes the given text in the specified color.
        /// </summary>
        /// <param name="prompt">Text to be written.</param>
        /// <param name="color">Color of the text.</param>
        void WriteColored(string prompt, ConsoleColor color);

        /// <summary>
        /// Writes the specified text followed by a line terminator to the console.
        /// </summary>
        /// <param name="text">The text to write to the console.</param>
        void WriteLine(string text);

        /// <summary>
        /// Writes the specified text to the console.
        /// </summary>
        /// <param name="text">The text to write to the console.</param>
        void Write(string text);

        /// <summary>
        /// Clears the conolse content.
        /// </summary>
        void Clear();
    }
}
