namespace Assignment9LINQAdvanced.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Assignment9LINQAdvanced.Database;
    using Assignment9LINQAdvanced.Models.Enums;

    /// <summary>
    /// Represents the Task 1 of the application.
    /// </summary>
    internal class Task1
    {
        private readonly ProductDatabase _productDatabase;

        public Task1(ProductDatabase productDatabase)
        {
            this._productDatabase = productDatabase;
        }

        /// <summary>
        /// Runs the Task 1 of the application.
        /// </summary>
        internal static void RunTask1()
        {
            ProductDatabase database = new ProductDatabase();

            // Query:
            // Filter products under the category "Electronics" with a price greater than $500 and select only ProductName and Price.
            // Using the result of the previous query, sort these filtered products in descending order of price.
            // Find the average price of these filtered products.
            var selectedProducts = database.GetAllProducts()
                .Where(p => p.Category == ProductCategory.Electronics && p.Price > 500)
                .Select(p => new { p.Name, p.Price })
                .OrderByDescending(p => p.Price)
                .ToList();

            decimal average = selectedProducts.Average(p => p.Price);

            // Display results
            Console.WriteLine("Task 1\n");
            Console.WriteLine($"{"Name",-20} | {"Price",10}");
            Console.WriteLine(new string('-', 33));
            foreach (var product in selectedProducts)
            {
                Console.WriteLine($"{product.Name,-20} | {product.Price,10:C}");
            }

            Console.WriteLine($"\nAVERAGE PRICE = {average:C}");
        }
    }
}
