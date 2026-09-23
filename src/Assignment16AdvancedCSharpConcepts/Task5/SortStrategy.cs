namespace Assignment16AdvancedCSharpConcepts.Task5
{
    /// <summary>
    /// Encapsulates the sorting strategies for comparing <see cref="Product"/> instances.
    /// </summary>
    internal class SortStrategy
    {
        /// <summary>
        /// Compares two products alphabetically by their name property.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer indicating the relative sort order based on the name.</returns>
        public int SortByName(Product product1, Product product2)
        {
            return product1.Name.CompareTo(product2.Name);
        }

        /// <summary>
        /// Compares two products alphabetically by their category string representation.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer indicating the relative sort order based on the category.</returns>
        public int SortByCategory(Product product1, Product product2)
        {
            return product1.Category.CompareTo(product2.Category);
        }

        /// <summary>
        /// Compares two products numerically by their price value.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer indicating the relative sort order based on the price.</returns>
        public int SortByPrice(Product product1, Product product2)
        {
            return product1.Price.CompareTo(product2.Price);
        }
    }
}
