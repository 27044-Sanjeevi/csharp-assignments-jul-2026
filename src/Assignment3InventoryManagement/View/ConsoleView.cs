namespace Assignment3InventoryManagement.View
{
    using Assignment3InventoryManagement.IO;
    using Assignment3InventoryManagement.Models;
    using Spectre.Console;

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
            return this.ReadString("Name of the Product : ") ?? string.Empty;
        }

        /// <summary>
        /// Reads the product name from the user.
        /// </summary>
        /// <returns>An integer holding the intial quantity of the product.</returns>
        public int GetProductQuantity()
        {
            return this.ReadInt("Initial quantity to add : ") ?? 0;
        }

        /// <summary>
        /// Reads the product price from the user.
        /// </summary>
        /// <returns>A decimal holding the price of the product.</returns>
        public decimal GetProductPrice()
        {
            return this.ReadDecimal("Price of the Product : ") ?? 0.0M;
        }

        /// <summary>
        /// Gets the name, allows empty or whitespace values.
        /// </summary>
        /// <returns>The name of the product, or empty string</returns>
        public string? GetOptionalName()
        {
            return this.ReadString("Name of the Product : ", isOptional: true);
        }

        /// <summary>
        /// Gets an optional product price, validating the input numeric bounds if entered.
        /// </summary>
        /// <returns>The parsed decimal value if a valid amount was entered; null if skipped.</returns>
        public decimal? GetOptionalPrice()
        {
            return this.ReadDecimal("New Price of the Product (Leave blank to keep unchanged): ", isOptional: true);
        }

        /// <summary>
        /// Gets an optional product stock quantity, validating the integer bounds if entered.
        /// </summary>
        /// <returns>The parsed integer value if a valid count was entered; null if skipped.</returns>
        public int? GetOptionalQuantity()
        {
            return this.ReadInt("New Stock Quantity (Leave blank to keep unchanged): ", isOptional: true);
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
        /// Displays the header for Add method.
        /// </summary>
        public void DisplayAddHeader()
        {
            this.PrintHeader("ADD A PRODUCT");
        }

        /// <summary>
        /// Displays the header for View operation.
        /// </summary>
        public void DisplayViewHeader()
        {
            this.PrintHeader("ALL CONTACTS LIST");
        }

        /// <summary>
        /// Displays the header for Search operation.
        /// </summary>
        public void DisplaySearchHeader()
        {
            this.PrintHeader("SEARCH RESULTS");
        }

        /// <summary>
        /// Displays a header indicating the update contact operation.
        /// </summary>
        public void DisplayUpdateHeader()
        {
            this.PrintHeader("UPDATE THE CONTACT");
            this.PrintSubHeader("Leave Blank Space for Unmodified Values.");
        }

        /// <summary>
        /// Displays an error message indicating that the product was not found.
        /// </summary>
        public void DisplayProductNotFound()
        {
            this.DisplayError("\nThe product is not found.\n");
        }

        /// <summary>
        /// Prompts the user to enter an existing product ID.
        /// </summary>
        /// <returns>The entered product ID as an integer, or 0 if no valid input is provided.</returns>
        public int GetIdFromUser()
        {
            return this.ReadInt("Enter the existing product Id : ") ?? 0;
        }

        /// <summary>
        /// Displays message on successfull product updation.
        /// </summary>
        /// <param name="id">Id of the updated product.</param>
        public void DisplayProductIsUpdated(int id)
        {
            this.WriteColored($"\nProduct with ID = {id} is updated successfully.\n", ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Displays the header for the deletion page.
        /// </summary>
        public void DisplayDeleteHeader()
        {
            this.PrintHeader("DELETION PAGE");
        }

        /// <summary>
        /// Displays a confirmation message indicating successful deletion of a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the deleted product.</param>
        public void DisplayProductIsDeleted(int id)
        {
            this.WriteColored($"\nProduct with ID = {id} is deleted successfully.\n", ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Displays products in a table.
        /// </summary>
        /// <param name="products">List of products to display.</param>
        public void DisplayAsTable(List<Product> products)
        {
            ArgumentNullException.ThrowIfNull(products);

            if (products.Count == 0)
            {
                this._consoleIo.WriteColored("\nNo products to display.\n", ConsoleColor.Red);
                return;
            }

            var table = new Table();

            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn(new TableColumn("Name").LeftAligned());
            table.AddColumn(new TableColumn("Price (Rs.)").RightAligned());
            table.AddColumn(new TableColumn("Quantity").RightAligned());

            foreach (Product product in products)
            {
                table.AddRow(
                    product.Id.ToString(),
                    product.Name,
                    $"{product.Price:F2}",
                    product.Quantity.ToString());
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Prompts for a search term.
        /// </summary>
        /// <returns>The search term entered by the user.</returns>
        public string GetSearchKeyword()
        {
            return this.ReadString("Enter search Keyword: ") ?? string.Empty;
        }

        /// <summary>
        /// Reads an integer value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null instead of a validation error.</param>
        /// <returns>The parsed integer value, or null if the field was skipped.</returns>
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
        /// Reads a decimal value from the console, optionally allowing an empty input to bypass validation.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="isOptional">If true, pressing Enter returns null. If false, it loops until a valid decimal is entered.</param>
        /// <returns>The parsed decimal value, or null if the field was skipped.</returns>
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

        /// <summary>
        /// A centralized method to ensure basic trimming.
        /// </summary>
        private string? ReadCleanLine(string prompt)
        {
            this._consoleIo.Write(prompt);
            string? input = this.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }
    }
}
