namespace Assignment3InventoryManagement.Models
{
    /// <summary>
    /// Represents a product in the inventory management system.
    /// </summary>
    internal class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        /// <value>The unique Id of the product as an integer.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>A String holding the name of the product.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>A decimal holding the price of the product.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in stock.
        /// </summary>
        /// <value>An integer holding the quantity of the product in stock.</value>
        public int Quantity { get; set; }
    }
}
