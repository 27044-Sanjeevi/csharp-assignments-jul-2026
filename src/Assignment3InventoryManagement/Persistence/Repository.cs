namespace Assignment3InventoryManagement.Persistence
{
    using Assignment3InventoryManagement.Models;

    /// <summary>
    /// Represents a repository for managing product data in the inventory management system.
    /// </summary>
    internal class Repository : IRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        /// <inheritdoc />
        public void Add(Product product)
        {
            product.Id = this._nextId++;
            this._products.Add(product);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public List<Product> GetAll()
        {
            List<Product> clonedProducts = new List<Product>();
            foreach (var product in this._products)
            {
                clonedProducts.Add(this.Clone(product));
            }

            return clonedProducts;
        }

        /// <inheritdoc />
        public int GetNextId()
        {
            return this._nextId;
        }

        /// <inheritdoc />
        public Product? GetById(int id)
        {
            return this._products.FirstOrDefault(p => p.Id == id);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
