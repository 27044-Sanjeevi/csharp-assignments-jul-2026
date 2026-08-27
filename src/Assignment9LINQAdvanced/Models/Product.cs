using Assignment9LINQAdvanced.Models.Enums;

namespace Assignment9LINQAdvanced.Models
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    internal class Product
    {
        private static int _idCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="id">Unique ID of the </param>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="category">Category of the product.</param>
        internal Product(int id, string name, decimal price, ProductCategory category)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }

        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        /// <value>A unique integer value for identifying the product..</value>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>A string holding the name of the product.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>A positive decimal holding the price of the product.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        /// <value>An enum holding the category of the product.</value>
        public ProductCategory Category { get; set; }
    }
}
