namespace Assignment3InventoryManagement.Services
{
    using System.Collections.Generic;
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Models.Enum;

    /// <summary>
    /// Defines business logic operations for managing product inventories.
    /// </summary>
    internal interface IInventoryService
    {
        /// <summary>
        /// Creates a new product with the specified identifier, name, price, and quantity.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="quantity">Quantity of the product in stock.</param>
        /// <returns>A new Product instance with the provided details.</returns>
        Product CreateProduct(string name, decimal price, int quantity);

        /// <summary>
        /// Creates a new Product with existing ID.
        /// </summary>
        /// <param name="id">Existing product Id.</param>
        /// <param name="name">Updated Name.</param>
        /// <param name="price">Updated price.</param>
        /// <param name="quantity">Updated quantity.</param>
        /// <returns>New product object with updated details.</returns>
        Product CreateUpdatedProduct(int id, string name, decimal price, int quantity);

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        string? AddProduct(Product product);

        /// <summary>
        /// Retrieves the products list.
        /// </summary>
        /// <returns>A list of all products in repository.</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        /// <param name="product">The product to remove.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        string? RemoveProduct(Product? product);

        /// <summary>
        /// Retrieves the product based on the Id provided.
        /// </summary>
        /// <param name="id">Id of the existing product.</param>
        /// <returns>The matched product object, else null.</returns>
        Product? GetProductById(int id);

        /// <summary>
        /// Updates the details of an existing product in the inventory.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <returns>A string which represents the validation error occurred, empty string if no error occurred.</returns>
        string UpdateProduct(Product product);

        /// <summary>
        /// Validates the specified product.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>The validation error message, or an empty string if the product is valid.</returns>
        string ValidateProduct(Product product);

        /// <summary>
        /// Searches for products whose properties match the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for within product properties.</param>
        /// <returns>A list of products that match the keyword.</returns>
        List<Product> SearchProducts(string keyword);

        /// <summary>
        /// Adds stock to an existing product in the inventory.
        /// </summary>
        /// <param name="id">The identifier of the product.</param>
        /// <param name="quantity">The quantity of stock to add.</param>
        /// <returns>A validation error message if the quantity to add is invalid or causes overflow; otherwise, an empty string.</returns>
        string? AddStock(int id, int quantity);

        /// <summary>
        /// Removes stock from an existing product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="quantity">The quantity of stock to remove.</param>
        /// <returns>A validation error message if the quantity to remove is invalid or exceeds available stock; otherwise, an empty string.</returns>
        string? RemoveStock(int id, int quantity);

        /// <summary>
        /// Gets all products sorted by the specified criteria and direction.
        /// </summary>
        /// <param name="sortField">The field option token to sort by (Id, Name, Price, Quantity).</param>
        /// <param name="isAscending">True to sort in ascending order; false for descending.</param>
        /// <returns>A sorted list of product models.</returns>
        List<Product> GetSortedProducts(SortField sortField, bool isAscending);
    }
}
