namespace Assignment3InventoryManagement.View
{
    using Assignment3InventoryManagement.IO;
    using Assignment3InventoryManagement.Models;
    using ConsoleTables;

    /// <summary>
    /// Handles presentation rendering, headers, menus, and user inputs for the console UI.
    /// </summary>
    internal class ConsoleView
    {
        private readonly ConsoleIO _consoleIo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleIo">The console view input output renderer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the Argument is null.</exception>
        public ConsoleView(ConsoleIO consoleIo)
        {
            this._consoleIo = consoleIo ?? throw new ArgumentNullException(nameof(ConsoleIO));
        }

        /// <summary>
        /// Displays the main application task menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this.WriteColored(
                "Available Operations:\n\n" +
                "1. Add a Product\n" +
                "2. View all Products\n" +
                "3. Search Product\n" +
                "4. Update a Product\n" +
                "5. Delete Product\n" +
                "6. Add stock\n" +
                "7. Remove stock\n" +
                "8. Exit Application\n\n",
                ConsoleColor.Cyan);
            this.Write("Choose the Task to run: ");
        }

        /// <summary>
        /// Reads the product name from the user.
        /// </summary>
        /// <returns>A string holding the name of the product.</returns>
        public string GetProductName()
        {
            return this.ReadString("Name of the Product : ");
        }

        /// <summary>
        /// Reads the product name from the user.
        /// </summary>
        /// <returns>An integer holding the intial quantity of the product.</returns>
        public int GetProductQuantity()
        {
            return this.ReadInt("Initial quantity to add : ");
        }

        /// <summary>
        /// Reads the product price from the user.
        /// </summary>
        /// <returns>A decimal holding the price of the product.</returns>
        public decimal GetProductPrice()
        {
            return this.ReadDecimal("Price of the Product : ");
        }

        /// <summary>
        /// Displays the Id of the product added.
        /// </summary>
        /// <param name="id">Id to be displayed.</param>
        public void DisplayId(int id)
        {
            this.WriteColored($"\n[NOTE] Product ID = {id}\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Displays products in a table.
        /// </summary>
        /// <param name="products">List of products to display.</param>
        public void DisplayAsTable(List<Product> products)
        {
            if (products.Count == 0)
            {
                this._consoleIo.WriteLine("No products to display.");
                return;
            }

            ConsoleTable table = new ConsoleTable("Id", "Name", "Price (Rs.)", "Quantity");

            foreach (Product product in products)
            {
                table.AddRow(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Quantity);
            }

            this._consoleIo.WriteLine(table.ToString());
        }

        /// <summary>
        /// Prompts the user for a valid positive int value.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The parsed integer value.</returns>
        public int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                this.Write(prompt);
                if (int.TryParse(this.ReadLine(), out value) && value >= 0.0)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid number. Please enter a positive integer value.\n", ConsoleColor.Red);
            }
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
                string? input = this.ReadLine(prompt);
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
                this.Write(prompt);
                if (double.TryParse(this.ReadLine(), out value) && value >= 0.0)
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
                if (decimal.TryParse(this.ReadLine(prompt), out value) && value >= 0.0M)
                {
                    return value;
                }

                this.WriteColored("[INPUT ERROR] Invalid amount. Please enter a positive decimal value.\n", ConsoleColor.Red);
            }
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
                this.Write(message);
            }

            while (!int.TryParse(this.ReadLine(), out result) || result < min || result > max)
            {
                this.WriteColored($"[INPUT ERROR] Invalid Choice. Choose an integer between {min} to {max}: ", ConsoleColor.Red);
            }

            return result;
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
        /// <param name="message">Message to be written on console.</param>
        public void WriteLine(string message)
        {
            this._consoleIo.WriteLine(message);
        }

        /// <summary>
        /// Reads a the input from the user as string.
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
            this.Write("\n" + new string('-', 40) + "\n\n");
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
    }
}
