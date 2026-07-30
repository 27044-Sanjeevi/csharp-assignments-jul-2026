namespace Assignment3InventoryManagement.Validation
{
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Services;

    /// <summary>
    /// Represents a validator for product-related operations in the inventory management system.
    /// </summary>
    internal class ProductValidation : IProductValidation
    {
        /// <summary>
        /// Validates the specified product based on its properties.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>a string containing the validation error message, or null if the product is valid.</returns>
        public string? ValidateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            if (product.Name == null || string.IsNullOrWhiteSpace(product.Name))
            {
                return "Product name cannot be empty.";
            }

            return product switch
            {
                { Name: { Length: < 3 or > 50 } } => "Product name must contain between 3 and 50 characters.",
                { Price: <= 0 } => "Product price must be greater than 0.",
                { Quantity: < 0 or > 1000 } => this.ValidateQuantity(product.Quantity),
                { Id: <= 0 } => "Product ID must be greater than 0.",
                _ => null
            };
        }

        /// <inheritdoc />
        public string? ValidateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                return "Net Stock quantity cannot be negative.";
            }

            if (quantity > 10000)
            {
                return "Stock quantity exceeds maximum allowable warehouse capacity.";
            }

            return null;
        }
    }
}
