namespace Assignment3InventoryManagement.Controller
{
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Models.Enum;
    using Assignment3InventoryManagement.Services;
    using Assignment3InventoryManagement.View;

    /// <summary>
    /// Coordinates operations between the UI/View and the Service layer.
    /// </summary>
    internal class MainController
    {
        private readonly ConsoleView _view;
        private readonly IInventoryService _inventoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="inventoryService">The service for inventory related operations.</param>
        public MainController(ConsoleView view, IInventoryService inventoryService)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view));
            this._inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        }

        /// <summary>
        /// Processes the selected menu option and executes the corresponding controller task.
        /// </summary>
        /// <param name="choice">The user's menu selection.</param>
        /// <returns> true to exit; false to continue displaying the menu.</returns>
        public bool HandleMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.AddProduct();
                    break;
                case 2:
                    this.ViewProducts();
                    break;
                case 3:
                    this.SearchProduct();
                    break;
                case 4:
                    this.UpdateProduct();
                    break;
                case 5:
                    this.RemoveProduct();
                    break;
                case 6:
                    this.AddStock();
                    break;
                case 7:
                    this.RemoveStock();
                    break;
                case 8:
                    this.SortProducts();
                    break;
                case 9:
                    this.PrintGoodBye();
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        public void AddProduct()
        {
            this._view.DisplayAddHeader();
            string name = this._view.GetProductName();
            decimal price = this._view.GetProductPrice();
            int quantity = this._view.GetProductQuantity();

            Product product = this._inventoryService.CreateProduct(name, price, quantity);

            string? errorMessage = this._inventoryService.AddProduct(product);

            this._view.ClearScreen();
            this._view.DisplayAddHeader();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                this._view.DisplayError(errorMessage);
                return;
            }

            this._view.DisplayProductIsAdded(product);
        }

        /// <summary>
        /// Displays all the products as a table.
        /// </summary>
        public void ViewProducts()
        {
            this._view.DisplayViewHeader();
            this.DisplayAllProductsTable();
        }

        /// <summary>
        /// Searches for products using a keyword and displays the matching results in a table.
        /// </summary>
        public void SearchProduct()
        {
            this._view.DisplaySearchHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            string keyword = this._view.GetSearchKeyword();
            List<Product> results = this._inventoryService.SearchProducts(keyword);

            this._view.ClearScreen();
            this._view.DisplaySearchHeader(keyword);
            this._view.DisplayAsTable(results, $"\nNo products found matching the keyword '{keyword}'.\n");
        }

        /// <summary>
        /// Updates the details of an existing product in the inventory.
        /// </summary>
        public void UpdateProduct()
        {
            this._view.DisplayUpdateHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            int? idToUpdate = this.GetExistingProductId();

            if (idToUpdate is null)
            {
                return;
            }

            Product? existingProduct = this._inventoryService.GetProductById(idToUpdate.Value);

            this._view.DisplaySingleProduct(existingProduct);

            if (existingProduct is not null)
            {
                string name = this._view.GetOptionalName(existingProduct.Name);
                decimal price = this._view.GetOptionalPrice(existingProduct.Price);
                Product updatedProduct = this._inventoryService.CreateUpdatedProduct(idToUpdate.Value, name, price, existingProduct.Quantity);
                string oldName = existingProduct.Name;
                decimal oldPrice = existingProduct.Price;
                string errorMessage = this._inventoryService.UpdateProduct(updatedProduct);

                this._view.ClearScreen();
                this._view.DisplayUpdateHeader();

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this._view.DisplayError(errorMessage);
                }
                else
                {
                    this._view.DisplayProductIsUpdated(idToUpdate.Value, oldName, oldPrice, updatedProduct);
                }
            }

            return;
        }

        /// <summary>
        /// Removes the product from the inventory.
        /// </summary>
        public void RemoveProduct()
        {
            this._view.DisplayDeleteHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            int? idToDelete = this.GetExistingProductId();

            if (idToDelete is null)
            {
                return;
            }

            Product? existingProduct = this._inventoryService.GetProductById(idToDelete.Value);
            string? errorMessage = this._inventoryService.RemoveProduct(existingProduct);

            this._view.ClearScreen();
            this._view.DisplayDeleteHeader();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                this._view.DisplayError(errorMessage);
            }
            else
            {
                this._view.DisplayProductIsDeleted(idToDelete.Value, existingProduct?.Name ?? string.Empty);
            }
        }

        /// <summary>
        /// Adds stock to a product in the inventory.
        /// </summary>
        public void AddStock()
        {
            this._view.DisplayAddStockHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            int? id = this.GetExistingProductId();

            if (id is null)
            {
                return;
            }

            this._view.DisplaySingleProduct(this._inventoryService.GetProductById(id.Value));

            Product? existingProduct = this._inventoryService.GetProductById(id.Value);
            int quantity = this._view.GetProductQuantityToAdd();
            string? errorMessage = this._inventoryService.AddStock(id.Value, quantity);

            this._view.ClearScreen();
            this._view.DisplayAddStockHeader();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                this._view.DisplayError(errorMessage);
            }
            else
            {
                int newQty = (existingProduct?.Quantity ?? 0) + quantity;
                this._view.PrintStockUpdation(id.Value, quantity);
                this._view.DisplaySingleProduct(this._inventoryService.GetProductById(id.Value));
            }
        }

        /// <summary>
        /// Removes stock from a product in the inventory.
        /// </summary>
        public void RemoveStock()
        {
            this._view.DisplayRemoveStockHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            int? id = this.GetExistingProductId();

            if (id is null)
            {
                return;
            }

            Product? existingProduct = this._inventoryService.GetProductById(id.Value);
            int quantity = this._view.GetProductQuantityToRemove();
            string? errorMessage = this._inventoryService.RemoveStock(id.Value, quantity);

            this._view.ClearScreen();
            this._view.DisplayRemoveStockHeader();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                this._view.DisplayError(errorMessage);
            }
            else
            {
                int newQty = (existingProduct?.Quantity ?? 0) - quantity;
                this._view.PrintStockUpdation(id.Value, quantity);
                this._view.DisplaySingleProduct(this._inventoryService.GetProductById(id.Value));
            }
        }

        /// <summary>
        /// Sorts products and displays the results.
        /// </summary>
        public void SortProducts()
        {
            this._view.SortProductsHeader();
            if (!this.TryDisplayInventory())
            {
                return;
            }

            SortField sortField = this._view.GetSortField();
            bool isAscending = this._view.GetSortDirection();
            List<Product> products = this._inventoryService.GetSortedProducts(sortField, isAscending);

            this._view.ClearScreen();
            this._view.DisplaySortHeader(sortField, isAscending);
            this._view.DisplayAsTable(products);
        }

        /// <summary>
        /// Prints a goodbye message.
        /// </summary>
        public void PrintGoodBye()
        {
            this._view.PrintGoodbye();
        }

        private bool TryDisplayInventory()
        {
            if (this._inventoryService.GetProductCount() <= 0)
            {
                this._view.DisplayNoProductsFound();
                return false;
            }

            this.DisplayAllProductsTable();
            return true;
        }

        private void DisplayAllProductsTable()
        {
            List<Product> products = this._inventoryService.GetAllProducts();
            this._view.DisplayAsTable(products);
        }

        /// <summary>
        /// Prompts the user for a product ID and validates its existence in the inventory.
        /// </summary>
        /// <returns>The validated product ID, or null if the product does not exist.</returns>
        private int? GetExistingProductId()
        {
            int id = this._view.GetIdFromUser();

            if (this._inventoryService.GetProductById(id) is null)
            {
                this._view.DisplayProductNotFound();
                return null;
            }

            return id;
        }
    }
}
