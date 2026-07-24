namespace BasicContactManagerAssignment1.IO
{
    using System;

    /// <summary>
    /// Provides concrete implementations of console input and output operations.
    /// </summary>
    internal class ConsoleIO
    {
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns>The next line of input from the console, or null if no lines are available.</returns>
        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Clears the console and writes a colored message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="color">The color of the message.</param>
        public void ClearAndWriteColored(string message, ConsoleColor color)
        {
            this.Clear();
            this.WriteColored(message, color);
        }

        /// <summary>
        /// Writes message to screen with custom color.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="color">The console color.</param>
        public void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            this.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Obtains the next character or function key pressed by the user.
        /// </summary>
        /// <param name="intercept">Determines whether to display the pressed key in the console window.</param>
        /// <returns>An object that describes the key that was pressed.</returns>
        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }
}
