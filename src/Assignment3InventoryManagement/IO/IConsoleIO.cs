namespace Assignment3InventoryManagement.IO
{
    using System;

    /// <summary>
    /// Defines input and output operations for console interactions.
    /// </summary>
    internal interface IConsoleIO
    {
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <param name="prompt">Prompt to be displayed to the user.</param>
        /// <returns>The next line of input from the console, or null if no lines are available.</returns>
        string? ReadLine(string? prompt);

        /// <summary>
        /// Writes message to screen with custom color.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="color">The console color.</param>
        void WriteColored(string message, ConsoleColor color);

        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void Write(string message);

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void WriteLine(string message);

        /// <summary>
        /// Clears the console window.
        /// </summary>
        void Clear();

        /// <summary>
        /// Obtains the next character or function key pressed by the user.
        /// </summary>
        /// <param name="intercept">Determines whether to display the pressed key in the console window.</param>
        /// <returns>An object that describes the key that was pressed.</returns>
        ConsoleKeyInfo ReadKey(bool intercept);
    }
}
