namespace Assignment3InventoryManagement.Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines generic console helper operations for input, output, formatting, and screen control.
    /// </summary>
    internal interface IConsoleHelper
    {
        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null instead of a validation error.</param>
        /// <returns>The parsed integer value, or null if the field was skipped.</returns>
        int? ReadInt(string prompt, bool isOptional = false);

        /// <summary>
        /// Reads a string value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null instead of a validation error.</param>
        /// <returns>The trimmed string input, or null if skipped.</returns>
        string? ReadString(string prompt, bool isOptional = false);

        /// <summary>
        /// Prompts the user for a valid positive double value.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The parsed double value.</returns>
        double ReadDouble(string prompt);

        /// <summary>
        /// Reads a decimal value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null. If false, it loops until a valid decimal is entered.</param>
        /// <returns>The parsed decimal value, or null if the field was skipped.</returns>
        decimal? ReadDecimal(string prompt, bool isOptional = false);

        /// <summary>
        /// Prompts the user continuously until they enter a valid choice in the specified range.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <param name="message">Optional message to be displayed.</param>
        /// <returns>A valid choice integer.</returns>
        int ReadChoice(int min, int max, string? message = null);

        /// <summary>
        /// Writes message in a custom console color.
        /// </summary>
        /// <param name="message">The text to write.</param>
        /// <param name="color">The target console color.</param>
        void WriteColored(string message, ConsoleColor color);

        /// <summary>
        /// Displays the message given.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        void Write(string message);

        /// <summary>
        /// Writes the message with a new line.
        /// </summary>
        /// <param name="message">Optional Message to be written on console.</param>
        void WriteLine(string message = "");

        /// <summary>
        /// Reads the input from the user as string.
        /// </summary>
        /// <param name="prompt">Optional prompt to be displayed.</param>
        /// <returns>The read string value.</returns>
        string? ReadLine(string? prompt = "");

        /// <summary>
        /// Prints a colored task header.
        /// </summary>
        /// <param name="title">The task title.</param>
        void PrintHeader(string title);

        /// <summary>
        /// Prints a colored sub-header.
        /// </summary>
        /// <param name="text">The sub-header text.</param>
        void PrintSubHeader(string text);

        /// <summary>
        /// Prints a divider line.
        /// </summary>
        void PrintDivider();

        /// <summary>
        /// Displays an error message in red.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        void DisplayError(string message);

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        void PrintGoodbye();

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        void PauseAndReturn();

        /// <summary>
        /// Clears the console window.
        /// </summary>
        void ClearScreen();

        /// <summary>
        /// Displays a selection menu using arrow keys and returns the index of the selected choice.
        /// </summary>
        /// <param name="title">The title prompt for selection.</param>
        /// <param name="choices">The list of choices to display.</param>
        /// <returns>The index of the selected choice (1-based).</returns>
        int ReadSelection(string title, List<string> choices);
    }
}
