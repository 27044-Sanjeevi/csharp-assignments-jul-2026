namespace Assignment3InventoryManagement.Controller
{
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Services;
    using Assignment3InventoryManagement.View;

    /// <summary>
    /// Coordinates operations between the UI/View and the Service layer.
    /// </summary>
    internal class MainController
    {
        private const int MinTaskChoice = 1;
        private const int MaxTaskChoice = 4;

        private readonly ConsoleView _view;
        private readonly InventoryService _inventoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer.</param>
        /// <param name="inventoryService">The service for inventory related operations.</param>
        public MainController(ConsoleView view, InventoryService inventoryService)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(View));
            this._inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(InventoryService));
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
                    // this.SearchProduct();
                    break;
                case 4:
                    // this.UpdateProduct();
                    break;
                case 5:
                    // this.DeleteProduct();
                    break;
                case 6:
                    // this.AddStock();
                    break;
                case 7:
                    // this.RemoveStock();
                    break;
                case 8:
                    // this._view.PrintGoodBye();
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        public void AddProduct()
        {
            string name = this._view.GetProductName();
            decimal price = this._view.GetProductPrice();
            int quantity = this._view.GetProductQuantity();

            Product product = this._inventoryService.CreateProduct(name, price, quantity);

            string? errorMessage = this._inventoryService.AddProduct(product);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                this._view.DisplayError(errorMessage);
                return;
            }

            this._view.DisplayId(product.Id);
        }


        public void ViewProducts()
        {
            List<Product> products = this._inventoryService.GetAllProducts();
            this._view.DisplayAsTable(products);
        }
    }
}
