namespace Assignment3InventoryManagement.Validation
{
    using Assignment3InventoryManagement.Models;

    /// <summary>
    /// Defines product validation logic.
    /// </summary>
    internal interface IProductValidation
    {
        /// <summary>
        /// Validates the specified product based on its properties.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>a string containing the validation error message, or null if the product is valid.</returns>
        string? ValidateProduct(Product product);

        /// <summary>
        /// Validates the quantity of the product.
        /// </summary>
        /// <param name="quantity">Quantity of the product.</param>
        /// <returns>The string describing the validation product.</returns>
        public string? ValidateQuantity(int quantity);
    }
}
