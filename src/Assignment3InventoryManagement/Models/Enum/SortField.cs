namespace Assignment3InventoryManagement.Models.Enum
{
    /// <summary>
    /// Represents the available fields that products can be sorted by.
    /// </summary>
    internal enum SortField
    {
        /// <summary>
        /// Sorts product by Id
        /// </summary>
        Id = 1,

        /// <summary>
        /// Sort products by name.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort products by price.
        /// </summary>
        Price = 3,

        /// <summary>
        /// Sort products by quantity.
        /// </summary>
        Quantity = 4,
    }
}
