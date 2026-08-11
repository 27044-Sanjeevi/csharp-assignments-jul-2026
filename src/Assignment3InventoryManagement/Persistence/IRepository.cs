namespace Assignment3InventoryManagement.Persistence
{
    using System.Collections.Generic;
    using Assignment3InventoryManagement.Models;

    /// <summary>
    /// Defines repository operations for managing product data persistence.
    /// </summary>
    internal interface IRepository
    {
        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        void Add(Product product);

        /// <summary>
        /// Removes a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to remove.</param>
        /// <returns>true if the product was found and removed; otherwise, false.</returns>
        bool Remove(int id);

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>The list of all products.</returns>
        IReadOnlyList<Product> GetAll();

        /// <summary>
        /// Retrieves the Id to be used next.
        /// </summary>
        /// <returns>The next product Id.</returns>
        int GetNextId();

        /// <summary>
        /// Retrieves a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with the specified identifier, or null if not found.</returns>
        Product? GetById(int id);

        /// <summary>
        /// Updates an existing product in the repository with new values.
        /// </summary>
        /// <param name="updatedProduct">The product with updated values.</param>
        /// <returns>true if the product was found and updated; otherwise, false.</returns>
        bool Update(Product updatedProduct);

        /// <summary>
        /// Creates a copy of the specified product.
        /// </summary>
        /// <param name="product">The product to clone.</param>
        /// <returns>A new instance of the product with the same properties.</returns>
        Product Clone(Product product);

        /// <summary>
        /// Retrieves the product count from the repository.
        /// </summary>
        /// <returns>An integer specifying the count of the products available in the list.</returns>
        int GetCount();
    }
}
