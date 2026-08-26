using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9LINQAdvanced
{
    internal static class ProductManager
    {
        internal static void RunTask1()
        {
            // Initialize list
            List<Product> products = new List<Product>()
            {
                new Product("Pen (black)", 20, ProductCategory.Stationery),
                new Product("Pencil", 8, ProductCategory.Stationery),
                new Product("Eraser", 5, ProductCategory.Stationery),
                new Product("Protractor", 15, ProductCategory.Stationery),
                new Product("Scale", 12, ProductCategory.Stationery),
                new Product("Pen (blue)", 20, ProductCategory.Stationery),

                new Product("Pan", 500, ProductCategory.KitchenUtility),
                new Product("Spatula", 100, ProductCategory.KitchenUtility),
                new Product("Bottle", 35, ProductCategory.KitchenUtility),
                new Product("Spoon", 30, ProductCategory.KitchenUtility),
                new Product("Fork", 30, ProductCategory.KitchenUtility),

                new Product("Realme Phone", 10000, ProductCategory.Electronics),
                new Product("Samsung Phone", 12000, ProductCategory.Electronics),
                new Product("Oppo Phone", 9000, ProductCategory.Electronics),
                new Product("Apple Phone", 50000, ProductCategory.Electronics),
                new Product("Oneplus Phone", 35000, ProductCategory.Electronics),
                new Product("Nothing Phone", 40000, ProductCategory.Electronics),
                new Product("Nokia Phone", 1000, ProductCategory.Electronics),

                new Product("Asus Laptop", 70000, ProductCategory.Electronics),
                new Product("Dell Laptop", 56000, ProductCategory.Electronics),
                new Product("Lenovo Laptop", 83000, ProductCategory.Electronics),
                new Product("Mac Laptop", 100000, ProductCategory.Electronics),
                new Product("Samsung Laptop", 66000, ProductCategory.Electronics),
                new Product("HP Laptop", 55000, ProductCategory.Electronics),

                new Product("Samsung Earphones", 499, ProductCategory.Electronics),
                new Product("Nothing Earphones", 501, ProductCategory.Electronics),
                new Product("Sony Earphones", 999, ProductCategory.Electronics),
                new Product("Apple Earphones", 801, ProductCategory.Electronics),
                new Product("Realme Earphones", 1099, ProductCategory.Electronics),

                new Product("Atomic Habits", 599, ProductCategory.Book),
                new Product("Power of Subconscious Mind", 799, ProductCategory.Book),
                new Product("Rich Dad Poor Dad", 200, ProductCategory.Book),
                new Product("The Atlas", 199, ProductCategory.Book),
                new Product("Harry Potter", 1239, ProductCategory.Book),
                new Product("Lord of the Rings", 899, ProductCategory.Book),
            };

            // Query:
            // Filter products under the category "Electronics" with a price greater than $500 and select only ProductName and Price.
            // Using the result of the previous query, sort these filtered products in descending order of price.
            // Find the average price of these filtered products.
            var selectedProducts = products
                .Where(p => p.Category == ProductCategory.Electronics && p.Price > 500)
                .Select(p => new { p.Name, p.Price })
                .ToList()
                .OrderByDescending(p => p.Price);

            decimal average = selectedProducts.Average(p => p.Price);

            // Display results
            Console.WriteLine("Task 1\n");
            Console.WriteLine($"{"Name",-20} | {"Price",10}");
            Console.WriteLine(new string('-', 33));
            foreach (var product in selectedProducts)
            {
                Console.WriteLine($"{product.Name, -20} | {product.Price, 10:C}");
            }

            Console.WriteLine($"\nAVERAGE PRICE = {average:C}");
        }
    }
}
