namespace Assignment16AdvancedCSharpConcepts.Task5
{
    /// <summary>
    /// Provides functionality to sort and display lists of products using custom sorting strategies.
    /// </summary>
    internal class ProductSorter
    {
        /// <summary>
        /// Represents a delegate used to compare two product objects for sorting evaluation.
        /// </summary>
        /// <param name="product1">The first product to compare.</param>
        /// <param name="product2">The second product to compare.</param>
        /// <returns>A signed integer that indicates the relative values of <paramref name="product1"/> and <paramref name="product2"/></returns>
        public delegate int SortDelegate(Product product1, Product product2);

        /// <summary>
        /// Sorts a copy of the product list based on the provided sorting strategy delegate and outputs the aligned table to the console.
        /// </summary>
        /// <param name="products">The original collection of products to process.</param>
        /// <param name="sortDelegate">The target sorting strategy used to compare the elements.</param>
        public void SortAndDisplay(List<Product> products, SortDelegate sortDelegate)
        {
            List<Product> clonedProducts = new List<Product>(products);
            clonedProducts.Sort(new Comparison<Product>(sortDelegate));
            ConsoleHelpers.WriteColored($"{"Name",-20} | {"Category",-12} | {"Price",7}\n", ConsoleColor.Cyan);
            Console.WriteLine(new string('-', 50));
            foreach (var product in clonedProducts)
            {
                Console.WriteLine($"{product.Name,-20} | {product.Category,12} | {product.Price,7}");
            }
        }
    }
}
