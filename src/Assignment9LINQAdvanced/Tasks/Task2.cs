using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents task 2 of the application.
    /// </summary>
    internal class Task2
    {
        private readonly ProductDatabase _productDatabase = new ProductDatabase();
        private readonly SupplierDatabase _supplierDatabase = new SupplierDatabase();

        /// <summary>
        /// Initializes a new instance of the <see cref="Task2"/> class.
        /// </summary>
        /// <param name="productDatabase"></param>
        /// <param name="supplierDatabase"></param>
        public Task2(ProductDatabase productDatabase, SupplierDatabase supplierDatabase)
        {
            this._productDatabase = productDatabase;
            this._supplierDatabase = supplierDatabase;
        }

        /// <summary>
        /// Runs the task 2 of the application.
        /// </summary>
        internal void RunTask2()
        {
            List<Product> products = this._productDatabase.GetAllProducts();
            List<Supplier> suppliers = this._supplierDatabase.GetAllSuppliers();
            var groupByQuery = products
                .GroupBy(product => product.Category);

            Console.WriteLine("\nTask 2\n");
            Console.WriteLine($"{"Id",-3} | {"Name",-20} | {"Price",15} | {"Category",-15} |");
            Console.WriteLine(new string('-', 50));

            foreach (var group in groupByQuery)
            {
                Console.WriteLine($"\nCATEGORY: {group.Key}");
                foreach (var product in group)
                {
                    Console.WriteLine($"{product.Id,-3} | {product.Name,-20} | {product.Price,15:C} | {product.Category,-15} |");
                }
            }

            var projectionQuery = groupByQuery
                .Select(group => new
                {
                    CategoryName = group.Key,
                    ProductCount = group.Count(),
                    ExpensiveItemPrice = group.Max(p => p.Price),
                    ExpensiveItemName = group.OrderByDescending(p => p.Price).First().Name,
                });

            Console.WriteLine($"\n{"Category Name",-15} | {"Product Count",-12} | {"Expensive Item Name",-19} | {"Expensive Item Price",15}");
            Console.WriteLine(new string('-', 65));
            foreach (var query in projectionQuery)
            {
                Console.WriteLine($"{query.CategoryName,-15} | {query.ProductCount,-12} | {query.ExpensiveItemName,-19} | {query.ExpensiveItemPrice,15:C}");
            }

            var joinQuery = suppliers
                .Join(
                    products,
                    supplier => supplier.ProductId,
                    product => product.Id,
                    (supplier, product) => new
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        SupplierName = supplier.SupplierName,
                        SupplierId = supplier.SupplierId,
                    }
                );

            Console.WriteLine("\nJoined Table:\n");
            Console.WriteLine($"{"Product Id",-10} | {"Product Name",-15} | {"Supplier ID",-10} | {"Supplier Name",-20}");
            Console.WriteLine(new string('-', 65));
            foreach (var result in joinQuery)
            {
                Console.WriteLine($"{result.ProductId,-10} | {result.ProductName,-15} | {result.SupplierId,-10} | {result.SupplierName,-20}");
            }
        }
    }
}
