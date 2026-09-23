namespace Assignment16AdvancedCSharpConcepts.Task5
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    internal class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        /// <param name="category">Category of the product.</param>
        /// <param name="price">Price of the product.</param>
        public Product(string name, ProductCategory category, decimal price)
        {
            this.Name = name;
            this.Category = category;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>A string holding the name of the product.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>An enum holding the category of the product.</value>
        public ProductCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>A decimal holding the price of the product.</value>
        public decimal Price { get; set; }
    }
}
