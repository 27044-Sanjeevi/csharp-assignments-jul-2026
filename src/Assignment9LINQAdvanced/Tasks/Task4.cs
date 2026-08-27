using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents task 4 of the application.
    /// </summary>
    internal class Task4
    {
        private readonly ProductDatabase _productDatabase = new ProductDatabase();

        /// <summary>
        /// Initializes a new instance of the <see cref="Task4"/> class.
        /// </summary>
        /// <param name="productDatabase">The product database instance used to query products.</param>
        public Task4(ProductDatabase productDatabase)
        {
            this._productDatabase = productDatabase;
        }

        /// <summary>
        /// Runs task 4.
        /// </summary>
        internal void RunTask4()
        {
            List<Product> products = this._productDatabase.GetAllProducts();

            // Console Tables Configuration
            ConsoleTableOptions options = new ConsoleTableOptions { EnableCount = false };

            // Warm up to ensure JIT compilation overhead doesn't skew results
            var warmUp = products.Where(product => product.Category == ProductCategory.Book).ToList();

            Stopwatch toListStopwatch = Stopwatch.StartNew();

            var toListResult = products
                .Where(product => product.Category == ProductCategory.Book)
                .OrderBy(product => product.Price)
                .ToList();
            toListStopwatch.Stop();

            ConsoleTable toListTable = new ConsoleTable("Product Name", "Price");
            foreach (var product in toListResult)
            {
                toListTable.AddRow(product.Name, $"{product.Price:C}");
            }

            Stopwatch iEnumerableStopWatch = Stopwatch.StartNew();

            var enumerableQuery = products
                .Where(product => product.Category == ProductCategory.Book)
                .OrderBy(product => product.Price);

            List<Product> enumerableResultList = new List<Product>();
            foreach (var product in enumerableQuery)
            {
                enumerableResultList.Add(product);
            }

            iEnumerableStopWatch.Stop();
            ConsoleTable enumerableTable = new ConsoleTable("Product Name", "Price");
            foreach (var product in enumerableResultList)
            {
                enumerableTable.AddRow(product.Name, $"{product.Price:C}");
            }

            Stopwatch lookupWatch = Stopwatch.StartNew();

            var categoryLookup = products.ToLookup(p => p.Category);
            var lookupResult = categoryLookup[ProductCategory.Book]
                .OrderBy(product => product.Price)
                .ToList();
            lookupWatch.Stop();

            Console.WriteLine("\n=== LINQ EXECUTION METRICS COMPARISON ===");

            var comparisonTable = new ConsoleTable("Execution Strategy", "Time (ms)", "Time (Ticks)");

            comparisonTable.AddRow(
                "1. Immediate (.ToList())",
                toListStopwatch.Elapsed.TotalMilliseconds.ToString("F4"),
                toListStopwatch.ElapsedTicks
            );

            comparisonTable.AddRow(
                "2. Deferred (IEnumerable Loop)",
                iEnumerableStopWatch.Elapsed.TotalMilliseconds.ToString("F4"),
                iEnumerableStopWatch.ElapsedTicks
            );

            comparisonTable.AddRow(
                "3. Indexed Lookup (.ToLookup())",
                lookupWatch.Elapsed.TotalMilliseconds.ToString("F4"),
                lookupWatch.ElapsedTicks
            );

            comparisonTable.Configure(opt => opt.EnableCount = false).Write();
        }
    }
}
