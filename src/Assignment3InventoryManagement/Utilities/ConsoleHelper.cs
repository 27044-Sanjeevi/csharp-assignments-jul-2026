namespace Assignment3InventoryManagement.Utilities
{
    using System;
    using System.Collections.Generic;
    using Assignment3InventoryManagement.IO;
    using Spectre.Console;

    /// <summary>
    /// Provides general console UI operations including colored outputs, headers, dividers, and validated input parsing.
    /// </summary>
    internal class ConsoleHelper : IConsoleHelper
    {
        private readonly IConsoleIO _consoleIo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHelper"/> class.
        /// </summary>
        /// <param name="consoleIo">The console I/O wrapper dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when consoleIo is null.</exception>
        public ConsoleHelper(IConsoleIO consoleIo)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(consoleIo));
        }

        /// <inheritdoc />
        public int? ReadInt(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);
                if (isOptional && string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (int.TryParse(input, out int value) && value >= 0)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive integer value.\n", ConsoleColor.Red);
            }
        }

        /// <inheritdoc />
        public string? ReadString(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);

                if (isOptional && string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                this.WriteColored("[INPUT ERROR] Input cannot be empty. Please try again.\n", ConsoleColor.Red);
            }
        }

        /// <inheritdoc />
        public double ReadDouble(string prompt)
        {
            double value;
            while (true)
            {
                this.Write(prompt);
                if (double.TryParse(this.ReadLine(), out value) && value >= 0.0)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive numeric value.\n", ConsoleColor.Red);
            }
        }

        /// <inheritdoc />
        public decimal? ReadDecimal(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);

                if (isOptional && string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (decimal.TryParse(input, out decimal value) && value >= 0.0M)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid amount. Please enter a positive decimal value.\n", ConsoleColor.Red);
            }
        }

        /// <inheritdoc />
        public int ReadChoice(int min, int max, string? message = null)
        {
            int result;

            if (message != null)
            {
                this.Write(message);
            }

            while (!int.TryParse(this.ReadLine(), out result) || result < min || result > max)
            {
                this.WriteColored($"[INPUT ERROR] Invalid Choice. Choose an integer between {min} to {max}: ", ConsoleColor.Red);
            }

            return result;
        }

        /// <inheritdoc />
        public void WriteColored(string message, ConsoleColor color)
        {
            this._consoleIo.WriteColored(message, color);
        }

        /// <inheritdoc />
        public void Write(string message)
        {
            this._consoleIo.Write(message);
        }

        /// <inheritdoc />
        public void WriteLine(string message)
        {
            this._consoleIo.WriteLine(message);
        }

        /// <inheritdoc />
        public string? ReadLine(string? prompt = "")
        {
            return this._consoleIo.ReadLine(prompt);
        }

        /// <inheritdoc />
        public void PrintHeader(string title)
        {
            AnsiConsole.Write(new Rule($"[bold cyan]{title}[/]") { Justification = Justify.Center });
            AnsiConsole.WriteLine();
        }

        /// <inheritdoc />
        public void PrintSubHeader(string text)
        {
            AnsiConsole.MarkupLine($"  [yellow]» {text}[/]");
            AnsiConsole.WriteLine();
        }

        /// <inheritdoc />
        public void PrintDivider()
        {
            this.Write("\n" + new string('-', 40) + "\n\n");
        }

        /// <inheritdoc />
        public void DisplayError(string message)
        {
            this.WriteColored(message, ConsoleColor.Red);
        }

        /// <inheritdoc />
        public void PrintGoodbye()
        {
            this.WriteLine("Press any key to exit the application...");
        }

        /// <inheritdoc />
        public void PauseAndReturn()
        {
            this.Write("\nPress any key to return to Main Page...");
            this._consoleIo.ReadKey(true);
        }

        /// <inheritdoc />
        public void ClearScreen()
        {
            this._consoleIo.Clear();
        }

        /// <inheritdoc />
        public int ReadSelection(string title, List<string> choices)
        {
            var prompt = new SelectionPrompt<string>()
                .Title(title)
                .HighlightStyle(new Style(Color.Black, Color.Aqua))
                .AddChoices(choices);

            string selected = AnsiConsole.Prompt(prompt);
            return choices.IndexOf(selected) + 1;
        }
    }
}
