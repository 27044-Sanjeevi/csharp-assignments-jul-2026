namespace InventoryManagementAssignment3.Services
{
    using InventoryManagementAssignment3.Models;
    using InventoryManagementAssignment3.Persistence;
    using InventoryManagementAssignment3.Validation;

    /// <summary>
    /// Represents a manager for handling product-related operations in the inventory management system.
    /// </summary>
    internal class InventoryManager
    {
        private readonly Repository _repository;
        private readonly ProductValidation _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryManager"/> class.
        /// </summary>
        /// <param name="repository">The repository to use for product operations.</param>
        /// <param name="validator">The validator to use for product validation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repository or validator is null.</exception>
        public InventoryManager(Repository repository, ProductValidation validator)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        public string? AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            string validationError = this.ValidateProduct(product);

            if (validationError is not null)
            {
                this._repository.Add(product);
            }

            return validationError;
        }

        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        /// <param name="product">The product to remove.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        public string RemoveProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            string validationError = this.ValidateProduct(product);
            this._repository.Remove(product.Id);

            return validationError;
        }

        /// <summary>
        /// Gets the count of a specific product in the inventory.
        /// </summary>
        /// <param name="product">The product for which to get the count.</param>
        /// <returns>The count of the product in the inventory, or -1 if the product is not found.</returns>
        public int GetProductCount(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            Product? existingProduct = this._repository.GetById(product.Id);
            return existingProduct?.Quantity ?? 0;
        }

        /// <summary>
        /// Updates the details of an existing product in the inventory.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <exception cref="ArgumentNullException">Thrown when the product is null.</exception>
        /// <returns>A string which represents the error occured, null if no error occured.</returns>
        public string UpdateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            Product? existingProduct = this._repository.GetById(product.Id);

            string validationError = this.ValidateProduct(product);

            if (existingProduct != null && string.IsNullOrEmpty(validationError))
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
            }

            return validationError;
        }

        /// <summary>
        /// Validates the specified product using the provided validator.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>The validation error message, or an empty string if the product is valid.</returns>
        public string ValidateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            return this._validator.ValidateProduct(product) ?? string.Empty;
        }
    }
}
