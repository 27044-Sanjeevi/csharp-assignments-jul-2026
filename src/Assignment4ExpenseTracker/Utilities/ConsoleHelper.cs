namespace Assignment4ExpenseTracker.Utilities
{
    using System.Collections.Generic;
    using Assignment4ExpenseTracker.IO;
    using Spectre.Console;

    /// <summary>
    /// Provides general console UI operations including colored outputs, headers, dividers, and validated input parsing.
    /// </summary>
    internal class ConsoleHelper
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

        /// <summary>
        /// Reads a positive integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null instead of a validation error.</param>
        /// <returns>The parsed integer value, or null if the field was skipped.</returns>
        public int? ReadPositiveInt(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);
                if (isOptional && string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive integer value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Reads a string value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null instead of a validation error.</param>
        /// <returns>The trimmed string input, or null if skipped.</returns>
        public string? ReadString(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt)?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (isOptional)
                    {
                        return null;
                    }

                    this.WriteColored("[INPUT ERROR] Input cannot be empty. Please try again.\n", ConsoleColor.Red);
                    continue;
                }

                return input;
            }
        }

        /// <summary>
        /// Reads a oositive decimal value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null. If false, it loops until a valid decimal is entered.</param>
        /// <returns>The parsed decimal value, or null if the field was skipped.</returns>
        public decimal? ReadPositiveDecimal(string prompt, bool isOptional = false)
        {
            while (true)
            {
                string? input = this.ReadLine(prompt);

                if (isOptional && string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (decimal.TryParse(input, out decimal value) && value > 0.0M)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive decimal value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Writes message in a custom console color.
        /// </summary>
        /// <param name="message">The text to write.</param>
        /// <param name="color">The target console color.</param>
        public void WriteColored(string message, ConsoleColor color)
        {
            this._consoleIo.WriteColored(message, color);
        }

        /// <summary>
        /// Displays the message given.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void Write(string message)
        {
            this._consoleIo.Write(message);
        }

        /// <summary>
        /// Writes the message with a new line.
        /// </summary>
        /// <param name="message">Optional Message to be written on console.</param>
        public void WriteLine(string message)
        {
            this._consoleIo.WriteLine(message);
        }

        /// <summary>
        /// Reads the input from the user as string.
        /// </summary>
        /// <param name="prompt">Optional prompt to be displayed.</param>
        /// <returns>The read string value.</returns>
        public string? ReadLine(string? prompt = "")
        {
            return this._consoleIo.ReadLine(prompt);
        }

        /// <summary>
        /// Prints a colored task header.
        /// </summary>
        /// <param name="title">The task title.</param>
        public void PrintHeader(string title)
        {
            AnsiConsole.Write(new Rule($"[bold cyan]{Markup.Escape(title)}[/]") { Justification = Justify.Center });
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Prints a colored sub-header.
        /// </summary>
        /// <param name="text">The sub-header text.</param>
        public void PrintSubHeader(string text)
        {
            AnsiConsole.MarkupLine($"  [yellow] => {Markup.Escape(text)}[/]");
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Displays an error message in red.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public void DisplayError(string message)
        {
            this.WriteColored(message, ConsoleColor.Red);
        }

        /// <summary>
        /// Displays the success message in green color.
        /// </summary>
        /// <param name="message">The message to be displayed,</param>
        public void DisplaySuccessMessage(string message)
        {
            this.WriteColored($"\n[SUCCESS] {message}\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        public void PrintGoodbye()
        {
            this.WriteLine("Press any key to exit the application...");
        }

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        public void PauseAndReturn()
        {
            this.Write("\nPress any key to return to Main Page...");
            this._consoleIo.ReadKey(true);
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void ClearScreen()
        {
            this._consoleIo.Clear();
        }

        /// <summary>
        /// Displays a selection menu using arrow keys and returns the index of the selected choice.
        /// </summary>
        /// <param name="title">The title prompt for selection.</param>
        /// <param name="choices">The list of choices to display.</param>
        /// <returns>The index of the selected choice (1-based).</returns>
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