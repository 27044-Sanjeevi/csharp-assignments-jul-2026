namespace Assignment3InventoryManagement.View
{
    using System;
    using System.Collections.Generic;
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Models.Enum;
    using Assignment3InventoryManagement.Utilities;
    using Spectre.Console;

    /// <summary>
    /// Handles presentation rendering, headers, menus, and user inputs for the console UI.
    /// </summary>
    internal class ConsoleView
    {
        private readonly IConsoleHelper _consoleHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleView"/> class.
        /// </summary>
        /// <param name="consoleHelper">The console helper for generic input, output, and formatting operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the consoleHelper is null.</exception>
        public ConsoleView(IConsoleHelper consoleHelper)
        {
            this._consoleHelper = consoleHelper ?? throw new ArgumentNullException(nameof(consoleHelper));
        }

        /// <summary>
        /// Displays the main application task menu.
        /// </summary>
        public void ShowMainMenu()
        {
            this._consoleHelper.PrintHeader("INVENTORY MANAGEMENT SYSTEM");

            var table = new Table()
                .Title("[bold cyan]Available Operations[/]")
                .Border(TableBorder.Rounded);

            table.AddColumn(new TableColumn("[yellow]Option[/]").Centered());
            table.AddColumn(new TableColumn("[yellow]Operation[/]").LeftAligned());
            table.AddColumn(new TableColumn("[yellow]Description[/]").LeftAligned());

            table.AddRow("1", "[bold]Add Product[/]", "Adds a new product with custom name, price, and initial quantity.");
            table.AddRow("2", "[bold]View Products[/]", "Displays all currently stored products in a structured table.");
            table.AddRow("3", "[bold]Search Product[/]", "Searches products by ID, name, price, or quantity keyword.");
            table.AddRow("4", "[bold]Update Product[/]", "Updates the name and price details of an existing product.");
            table.AddRow("5", "[bold]Delete Product[/]", "Removes a product completely from the inventory.");
            table.AddRow("6", "[bold]Add Stock[/]", "Replenishes stock quantity of an existing product.");
            table.AddRow("7", "[bold]Remove Stock[/]", "Deducts stock quantity of an existing product.");
            table.AddRow("8", "[bold]Sort Products[/]", "Sorts the Products.");
            table.AddRow("9", "[bold]Exit[/]", "Exits the Application.");

            AnsiConsole.Write(table);
            this._consoleHelper.WriteLine(string.Empty);
        }

        /// <summary>
        /// Reads the product name from the user.
        /// </summary>
        /// <returns>A string holding the name of the product.</returns>
        public string GetProductName()
        {
            return this._consoleHelper.ReadString("Name of the Product : ") ?? string.Empty;
        }

        /// <summary>
        /// Reads the product name from the user.
        /// </summary>
        /// <returns>An integer holding the intial quantity of the product.</returns>
        public int GetProductQuantity()
        {
            return this._consoleHelper.ReadInt("Initial quantity to add : ") ?? 0;
        }

        /// <summary>
        /// Reads the product price from the user.
        /// </summary>
        /// <returns>A decimal holding the price of the product.</returns>
        public decimal GetProductPrice()
        {
            return this._consoleHelper.ReadDecimal("Price of the Product : ") ?? 0.0M;
        }

        /// <summary>
        /// Gets the name, allows empty or whitespace values.
        /// </summary>
        /// <param name="name">Existing name of the product.</param>
        /// <returns>The new name of the product, or the existing name.</returns>
        public string GetOptionalName(string name)
        {
            return this._consoleHelper.ReadString($"New Name of the Product [Existing: {name}] : ", isOptional: true) ?? name;
        }

        /// <summary>
        /// Gets an optional product price, validating the input numeric bounds if entered.
        /// </summary>
        /// <param name="price">Existing price of the product.</param>
        /// <returns>The parsed decimal value if a valid amount was entered; null if skipped.</returns>
        public decimal GetOptionalPrice(decimal price)
        {
            return this._consoleHelper.ReadDecimal($"New Price of the Product [Existing: {price}] : ", isOptional: true) ?? price;
        }

        /// <summary>
        /// Prompts the user to enter the quantity of stock to add.
        /// </summary>
        /// <returns>The quantity to add.</returns>
        public int GetProductQuantityToAdd()
        {
            return this._consoleHelper.ReadInt("Enter quantity to add to stock: ") ?? 0;
        }

        /// <summary>
        /// Prompts the user to enter the quantity of stock to remove.
        /// </summary>
        /// <returns>The quantity to remove.</returns>
        public int GetProductQuantityToRemove()
        {
            return this._consoleHelper.ReadInt("Enter quantity to remove from stock: ") ?? 0;
        }

        /// <summary>
        /// Displays the Id of the product added.
        /// </summary>
        /// <param name="id">Id to be displayed.</param>
        public void DisplayId(int id)
        {
            this._consoleHelper.WriteColored($"\n[NOTE] Product ID = {id}\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Displays the header for Add method.
        /// </summary>
        public void DisplayAddHeader()
        {
            this._consoleHelper.PrintHeader("ADD A PRODUCT");
        }

        /// <summary>
        /// Displays the header for View operation.
        /// </summary>
        public void DisplayViewHeader()
        {
            this._consoleHelper.PrintHeader("ALL PRODUCTS LIST");
        }

        /// <summary>
        /// Displays the header for Search operation.
        /// </summary>
        public void DisplaySearchHeader()
        {
            this._consoleHelper.PrintHeader("SEARCH RESULTS");
        }

        /// <summary>
        /// Displays a header indicating the update product operation.
        /// </summary>
        public void DisplayUpdateHeader()
        {
            this._consoleHelper.PrintHeader("UPDATE THE PRODUCT");
            this._consoleHelper.PrintSubHeader("Leave Blank Space for Unmodified Values.");
        }

        /// <summary>
        /// Displays a header indicating the add stock operation.
        /// </summary>
        public void DisplayAddStockHeader()
        {
            this._consoleHelper.PrintHeader("ADD STOCK TO A PRODUCT");
        }

        /// <summary>
        /// Displays a header indicating the remove stock operation.
        /// </summary>
        public void DisplayRemoveStockHeader()
        {
            this._consoleHelper.PrintHeader("REMOVE STOCK FROM A PRODUCT");
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
            return this._consoleHelper.ReadInt("Enter the existing product Id : ") ?? 0;
        }

        /// <summary>
        /// Gets selected sort field.
        /// </summary>
        /// <returns>The selected sort field as a SortField enum.</returns>
        public SortField GetSortField()
        {
            this._consoleHelper.PrintHeader("Sort Products");

            this._consoleHelper.WriteLine("Sort by:");
            this._consoleHelper.WriteLine("1. Id");
            this._consoleHelper.WriteLine("2. Name");
            this._consoleHelper.WriteLine("3. Price");
            this._consoleHelper.WriteLine("4. Quantity");

            int choice = this._consoleHelper.ReadChoice(1, 4, "Choose sort field (1-4): ");

            return (SortField)choice;
        }

        /// <summary>
        /// Gets selected sort direction.
        /// </summary>
        /// <returns>True for ascending, false for descending.</returns>
        public bool GetSortDirection()
        {
            this._consoleHelper.WriteLine();
            this._consoleHelper.WriteLine("Sort direction:");
            this._consoleHelper.WriteLine("1. Ascending");
            this._consoleHelper.WriteLine("2. Descending");

            int choice = this._consoleHelper.ReadChoice(1, 2, "Choose direction (1-2): ");

            return choice == 1;
        }

        /// <summary>
        /// Displays message on successfull product updation.
        /// </summary>
        /// <param name="id">Id of the updated product.</param>
        public void DisplayProductIsUpdated(int id)
        {
            this._consoleHelper.WriteColored($"\nProduct with ID = {id} is updated successfully.\n", ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Displays the header for the deletion page.
        /// </summary>
        public void DisplayDeleteHeader()
        {
            this._consoleHelper.PrintHeader("DELETION PAGE");
        }

        /// <summary>
        /// Displays a confirmation message indicating successful deletion of a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the deleted product.</param>
        public void DisplayProductIsDeleted(int id)
        {
            this._consoleHelper.WriteColored($"\nProduct with ID = {id} is deleted successfully.\n", ConsoleColor.DarkGreen);
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
                this._consoleHelper.WriteColored("\nNo products to display.\n", ConsoleColor.Red);
                return;
            }

            var table = new Table();

            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn(new TableColumn("Name").LeftAligned());
            table.AddColumn(new TableColumn("Price (Rs.)").LeftAligned());
            table.AddColumn(new TableColumn("Quantity").LeftAligned());

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
            return this._consoleHelper.ReadString("Enter search Keyword: ") ?? string.Empty;
        }

        /// <summary>
        /// Displays an error message in red.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public void DisplayError(string message)
        {
            this._consoleHelper.DisplayError(message);
        }

        /// <summary>
        /// Clears the console window.
        /// </summary>
        public void ClearScreen()
        {
            this._consoleHelper.ClearScreen();
        }

        /// <summary>
        /// Prompts the user to return to the main menu page.
        /// </summary>
        public void PauseAndReturn()
        {
            this._consoleHelper.PauseAndReturn();
        }

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        public void PrintGoodbye()
        {
            this._consoleHelper.PrintGoodbye();
        }

        /// <summary>
        /// Prints the stock updation message.
        /// </summary>
        public void PrintStockUpdation()
        {
            this._consoleHelper.WriteColored("Stock Updated Successfully.", ConsoleColor.Green);
        }

        /// <summary>
        /// Prompts the user continuously using arrow keys to select a menu option.
        /// </summary>
        /// <param name="min">The minimum valid choice.</param>
        /// <param name="max">The maximum valid choice.</param>
        /// <returns>A valid choice integer corresponding to the selection index.</returns>
        public int ReadChoice(int min, int max)
        {
            var choices = new List<string>
            {
                "1. Add Product",
                "2. View Products",
                "3. Search Product",
                "4. Update Product",
                "5. Delete Product",
                "6. Add Stock",
                "7. Remove Stock",
                "8. Sort Products",
                "9. Exit",
            };

            return this._consoleHelper.ReadSelection("Select an operation to run:", choices);
        }
    }
}
