namespace Assignment2BasicsOfOOPs.View
{
    using System;
    using Assignment2BasicsOfOOPs.Models;

    /// <summary>
    /// Handles presentation rendering, headers, menus, and user inputs for the console UI.
    /// </summary>
    internal class ConsoleView
    {
        /// <summary>
        /// Displays the main application task menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this.WriteColored(
                "Available Tasks:\n" +
                "1. Task 1 - Shape Hierarchy\n" +
                "2. Task 2 - Employee Hierarchy\n" +
                "3. Task 3 - Bank System\n" +
                "4. Exit Application\n\n",
                ConsoleColor.Cyan);
            this.Display("Choose the Task to run: ");
        }

        /// <summary>
        /// Prompts the user continuously until they enter a valid choice in the specified range.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <param name="message">Optional message to be displayed.</param>
        /// <returns>A valid choice integer.</returns>
        public int ReadChoice(int min, int max, string? message = null)
        {
            int result;

            if (message != null)
            {
                this.Display(message);
            }

            while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
            {
                this.WriteColored($"[INPUT ERROR] Invalid Choice. Choose an integer between {min} to {max}: ", ConsoleColor.Red);
            }

            return result;
        }

        /// <summary>
        /// Displays the banking submenu options for an active account.
        /// </summary>
        public void ShowBankMenu()
        {
            this.Display("\nAvailable Actions:\n");
            this.Display("1. Deposit Money\n");
            this.Display("2. Withdraw Money\n");
            this.Display("3. Print Account Details\n");
            this.Display("4. Return to Task Menu\n");
            this.Display("Choose Action (1-4): ");
        }

        /// <summary>
        /// Prompts the user for a non-empty string.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The validated string input.</returns>
        public string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                this.WriteColored("[INPUT ERROR] Input cannot be empty. Please try again.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Prompts the user for a valid positive double value.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The parsed double value.</returns>
        public double ReadDouble(string prompt)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out value) && value >= 0.0)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive numeric value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Prompts the user for a valid positive decimal value.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The parsed decimal value.</returns>
        public decimal ReadDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value) && value >= 0.0M)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid amount. Please enter a positive decimal value.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Prints a colored task header.
        /// </summary>
        /// <param name="title">The task title.</param>
        public void PrintHeader(string title)
        {
            this.WriteColored($"=== {title} ===\n\n", ConsoleColor.Blue);
        }

        /// <summary>
        /// Prints a colored sub-header.
        /// </summary>
        /// <param name="text">The sub-header text.</param>
        public void PrintSubHeader(string text)
        {
            this.WriteColored($"{text}\n", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Prints a divider line.
        /// </summary>
        public void PrintDivider()
        {
            Console.Write("\n" + new string('-', 40) + "\n\n");
        }

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        public void PrintGoodbye()
        {
            Console.WriteLine("Press any key to exit the application...");
        }

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        public void PauseAndReturn()
        {
            Console.Write("\nPress any key to return to Main Page...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Formats and prints shape details.
        /// </summary>
        /// <param name="shape">The shape model.</param>
        /// <param name="area">The calculated area of the shape.</param>
        public void PrintShapeDetails(Shape shape, double area)
        {
            if (shape == null)
            {
                return;
            }

            Console.WriteLine($"Shape Type: {shape.ShapeType}\nColor: {shape.Color}\nArea: {area:F2}\n");
        }

        /// <summary>
        /// Prints the error message on the wrong value of the color of the shape.
        /// </summary>
        public void PrintInvalidShapeColor()
        {
            this.WriteColored("The color of the shape can contain only alphabets.", ConsoleColor.Red);
        }

        /// <summary>
        /// Formats and prints employee details by fetching the model's details string.
        /// </summary>
        /// <param name="employee">The employee model.</param>
        public void PrintEmployeeDetails(Employee employee)
        {
            if (employee == null)
            {
                return;
            }

            Console.Write(employee.GetDetails());
        }

        /// <summary>
        /// Formats and prints bank account details by fetching the model's details string.
        /// </summary>
        /// <param name="account">The bank account model.</param>
        public void PrintBankAccountDetails(BankAccount account)
        {
            if (account == null)
            {
                return;
            }

            Console.Write(account.GetDetails());
        }

        /// <summary>
        /// Prints the current balance of a bank account by fetching the balance string.
        /// </summary>
        /// <param name="account">The bank account model.</param>
        public void PrintBankAccountBalance(BankAccount account)
        {
            if (account == null)
            {
                return;
            }

            Console.Write(account.GetBalanceDetails());
        }

        /// <summary>
        /// Prints a successful deposit message.
        /// </summary>
        /// <param name="amount">The deposit amount.</param>
        public void PrintDepositSuccess(decimal amount)
        {
            Console.WriteLine($"Successfully Deposited: Rs. {amount:F2}");
        }

        /// <summary>
        /// Prints a withdrawal attempt message.
        /// </summary>
        /// <param name="amount">The withdrawal amount.</param>
        public void PrintWithdrawAttempt(decimal amount)
        {
            Console.WriteLine($"Attempting to withdraw: Rs. {amount:F2}");
        }

        /// <summary>
        /// Prints a successful withdrawal message.
        /// </summary>
        /// <param name="amount">The withdrawal amount.</param>
        public void PrintWithdrawSuccess(decimal amount)
        {
            this.WriteColored($"Withdrawn successfully: Rs. {amount:F2}\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Prints a failed withdrawal transaction block.
        /// </summary>
        /// <param name="message">The failure reason.</param>
        public void PrintWithdrawFailure(string message)
        {
            this.WriteColored($"[TRANSACTION BLOCKED] {message}\n", ConsoleColor.Red);
            Console.WriteLine("Current status is not affected.");
        }

        /// <summary>
        /// Writes message in a custom console color.
        /// </summary>
        /// <param name="message">The text to write.</param>
        /// <param name="color">The target console color.</param>
        public void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the message given.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void Display(string message)
        {
            Console.Write(message);
        }
    }
}
