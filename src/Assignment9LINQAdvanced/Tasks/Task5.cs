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
        private readonly SupplierDatabase _supplierDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task5"/> class.
        /// </summary>
        /// <param name="productDatabase">An instance of the product database.</param>
        /// <param name="supplierDatabase">An instance of the supplier database.</param>
        public Task5(ProductDatabase productDatabase, SupplierDatabase supplierDatabase)
        {
            this._productDatabase = productDatabase;
            this._supplierDatabase = supplierDatabase;
        }

        /// <summary>
        /// Runs the custom query builder pipeline and displays the formatted results.
        /// </summary>
        internal void RunTask5()
        {
            List<Product> products = this._productDatabase.GetAllProducts();
            List<Supplier> suppliers = this._supplierDatabase.GetAllSuppliers();

            Console.WriteLine("Task 5\n");
            var productQueryBuilder = new QueryBuilder<Product>(products);
            var finalResult = productQueryBuilder
                .Join(
                suppliers,
                product => product.Id,
                supplier => supplier.ProductId,
                (product, supplier) => new
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    SupplierName = supplier.SupplierName,
                    Category = product.Category,
                    Price = product.Price,
                })
                .Filter(joined => joined.Category == ProductCategory.Electronics)
                .Filter(joined => joined.Price > 1000m)
                .SortBy(joined => joined.Price)
                .Execute();

            var table = new ConsoleTable("Id", "Product Name", "Supplier Name", "Price", "Category");
            foreach (var joinedProductSupplier in finalResult)
            {
                table.AddRow(
                    joinedProductSupplier.ProductId,
                    joinedProductSupplier.ProductName,
                    joinedProductSupplier.SupplierName,
                    $"{joinedProductSupplier.Price:C}",
                    joinedProductSupplier.Category);
            }

            table.Configure(options => options.EnableCount = false).Write();
            productQueryBuilder = new QueryBuilder<Product>(products);
            var containsResult = productQueryBuilder
                .Contains("Name", "Laptop")
                .GreaterThanOrEqualTo("Price", 50000m)
                .LessThanOrEqualTo("Price", 75000m)
                .Execute();

            var containsTable = new ConsoleTable("Laptop Name", "Price");
            foreach (var laptop in containsResult)
            {
                containsTable.AddRow(
                    laptop.Name,
                    laptop.Price);
            }

            Console.WriteLine("Using Contains, Greater than equal to, Lesser than equal to methods:");
            containsTable.Configure(options => options.EnableCount = false).Write();

            productQueryBuilder = new QueryBuilder<Product>(products);
            var startsWithResult = productQueryBuilder
                .StartsWith("Name", "Apple")
                .Execute();
            var startsWithTable = new ConsoleTable("Apple Product Name", "Product Category");
            foreach (var product in startsWithResult)
            {
                startsWithTable.AddRow(
                    product.Name,
                    product.Category);
            }

            Console.WriteLine("Using StartsWith method:");
            startsWithTable.Configure(options => options.EnableCount = false).Write();

            productQueryBuilder = new QueryBuilder<Product>(products);

            var endsWithResult = productQueryBuilder
                .EndsWith("Name", "Earphones")
                .Execute();

            var endsWithTable = new ConsoleTable("Earphone Product Name", "Price");
            foreach (var product in endsWithResult)
            {
                endsWithTable.AddRow(
                    product.Name,
                    product.Price);
            }

            Console.WriteLine("Using EndsWith method:");
            endsWithTable.Configure(options => options.EnableCount = false).Write();
        }
    }
}
