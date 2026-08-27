using System;
using System.Collections.Generic;
using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Handles the execution and demonstration of Task 5 (Custom Query Builder Pattern).
    /// </summary>
    internal class Task5
    {
        private readonly ProductDatabase _productDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task5"/> class.
        /// </summary>
        /// <param name="productDatabase">An instance of the product database.</param>
        public Task5(ProductDatabase productDatabase)
        {
            this._productDatabase = productDatabase;
        }

        /// <summary>
        /// Runs the custom query builder pipeline and displays the formatted results.
        /// </summary>
        internal void RunTask5()
        {
            List<Product> products = this._productDatabase.GetAllProducts();

            Console.WriteLine("Task 5\n");
            var productQueryBuilder = new QueryBuilder<Product>(products);
            List<Product> finalResult = productQueryBuilder
                .Filter(product => product.Category == ProductCategory.Electronics)
                .Filter(product => product.Price > 1000m)
                .SortBy(product => product.Price)
                .Execute();

            var table = new ConsoleTable("Id", "Product Name", "Price", "Category");
            foreach (var product in finalResult)
            {
                table.AddRow(
                    product.Id,
                    product.Name,
                    $"{product.Price:C}",
                    product.Category);
            }

            table.Configure(options => options.EnableCount = false).Write();
        }
    }
}
