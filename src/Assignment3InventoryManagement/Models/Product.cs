namespace InventoryManagementAssignment3.Models
{
    /// <summary>
    /// Represents a product in the inventory management system.
    /// </summary>
    internal class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in stock.
        /// </summary>
        public int Quantity { get; set; }
    }
}
