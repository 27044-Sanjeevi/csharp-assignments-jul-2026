using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment8ErrorHandling.IO
{
    /// <summary>
    /// Provides methods console related input and output operations.
    /// </summary>
    internal class ConsoleIO : IConsoleIO
    {
        /// <inheritdoc />
        public string? ReadLine(string? prompt)
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                this.Write(prompt);
            }

            return Console.ReadLine();
        }

        /// <inheritdoc />
        public void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <inheritdoc />
        public void Write(string text)
        {
            Console.Write(text);
        }

        /// <inheritdoc />
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Console.Clear();
        }
    }
}
