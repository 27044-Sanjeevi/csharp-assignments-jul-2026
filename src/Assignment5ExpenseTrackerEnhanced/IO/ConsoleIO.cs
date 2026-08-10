namespace Assignment5ExpenseTrackerEnhanced.IO
{
    /// <summary>
    /// Provides concrete implementations of console input and output operations.
    /// </summary>
    internal class ConsoleIO : IIo
    {
        /// <inheritdoc />
        public string? ReadLine(string? prompt)
        {
            if (prompt != null)
            {
                this.Write(prompt);
            }

            return Console.ReadLine();
        }

        /// <inheritdoc />
        public void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            this.Write(message);
            Console.ResetColor();
        }

        /// <inheritdoc />
        public void Write(string message)
        {
            Console.Write(message);
        }

        /// <inheritdoc />
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Console.Clear();
        }

        /// <inheritdoc />
        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }
}
