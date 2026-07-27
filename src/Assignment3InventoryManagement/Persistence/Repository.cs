namespace Assignment3InventoryManagement.Persistence
{
    using Assignment3InventoryManagement.Models;

    /// <summary>
    /// Represents a repository for managing product data in the inventory management system.
    /// </summary>
    internal class Repository
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void Add(Product product)
        {
            this._products.Add(product);
        }

        /// <summary>
        /// Removes a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to remove.</param>
        /// <returns>true if the product was found and removed; otherwise, false.</returns>
        public bool Remove(int id)
        {
            var product = this._products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                this._products.Remove(product);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>The list of all products.</returns>
        public List<Product> GetAll()
        {
            List<Product> clonedProducts = new List<Product>();
            foreach (var product in this._products)
            {
                clonedProducts.Add(this.Clone(product));
            }

            return clonedProducts;
        }

        /// <summary>
        /// Retrieves the Id to be used next.
        /// </summary>
        /// <returns>The next product Id.</returns>
        public int GetNextId()
        {
            return this._nextId++;
        }

        /// <summary>
        /// Retrieves a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with the specified identifier, or null if not found.</returns>
        public Product? GetById(int id)
        {
            return this._products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Creates a copy of the specified product.
        /// </summary>
        /// <param name="product">The product to clone.</param>
        /// <returns>A new instance of the product with the same properties.</returns>
        public Product Clone(Product product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };
        }

        /// <summary>
        /// Updates an existing product in the repository with new values.
        /// </summary>
        /// <param name="updatedProduct">The product with updated values.</param>
        /// <returns>true if the product was found and updated; otherwise, false.</returns>
        public bool Update(Product updatedProduct)
        {
            var existingProduct = this.GetById(updatedProduct.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                return true;
            }

            return false;
        }
    }
}
