namespace InventoryManagementAssignment3.Validation
{
    using InventoryManagementAssignment3.Models;
    using InventoryManagementAssignment3.Services;

    /// <summary>
    /// Represents a validator for product-related operations in the inventory management system.
    /// </summary>
    internal class ProductValidation
    {
        private readonly InventoryManager _productManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductValidation"/> class.
        /// </summary>
        /// <param name="productManager">The product manager.</param>
        /// <exception cref="ArgumentNullException">Thrown when the product manager is null.</exception>
        public ProductValidation(InventoryManager productManager)
        {
            this._productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        /// <summary>
        /// Validates the specified product based on its properties.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>a string containing the validation error message, or null if the product is valid.</returns>
        public string? ValidateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            return product switch
            {
                { Name: null } => "Product name cannot be empty.",
                { Name: {Length: < 3 or > 50 } } => "Product name must contain between 3 and 50 characters.",
                { Price: <= 0 } => "Product price must be greater than 0.",
                { Quantity: <= 0 } => "Product quantity must be greater than 0.",
                { Id: <= 0 } => "Product ID must be greater than 0.",
                _ => null
            };
        }
    }
}
