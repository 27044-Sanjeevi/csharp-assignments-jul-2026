using System.Collections.Generic;
using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;

namespace Assignment9LINQAdvanced.Database
{
    /// <summary>
    /// Represents the database for all tasks.
    /// </summary>
    internal class ProductDatabase
    {
        private readonly List<Product> _products;

        private int _idCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDatabase"/> class.
        /// </summary>
        public ProductDatabase()
        {
            this._products = new List<Product>()
            {
                new Product(++this._idCounter, "Pen (black)", 20, ProductCategory.Stationery),
                new Product(++this._idCounter, "Pencil", 8, ProductCategory.Stationery),
                new Product(++this._idCounter, "Eraser", 5, ProductCategory.Stationery),
                new Product(++this._idCounter, "Protractor", 15, ProductCategory.Stationery),
                new Product(++this._idCounter, "Scale", 12, ProductCategory.Stationery),
                new Product(++this._idCounter, "Pen (blue)", 20, ProductCategory.Stationery),

                new Product(++this._idCounter, "Pan", 500, ProductCategory.KitchenUtility),
                new Product(++this._idCounter, "Spatula", 100, ProductCategory.KitchenUtility),
                new Product(++this._idCounter, "Bottle", 35, ProductCategory.KitchenUtility),
                new Product(++this._idCounter, "Spoon", 30, ProductCategory.KitchenUtility),
                new Product(++this._idCounter, "Fork", 30, ProductCategory.KitchenUtility),

                new Product(++this._idCounter, "Realme Phone", 10000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Samsung Phone", 12000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Oppo Phone", 9000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Apple Phone", 50000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Oneplus Phone", 35000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Nothing Phone", 40000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Nokia Phone", 1000, ProductCategory.Electronics),

                new Product(++this._idCounter, "Asus Laptop", 70000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Dell Laptop", 56000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Lenovo Laptop", 83000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Mac Laptop", 100000, ProductCategory.Electronics),
                new Product(++this._idCounter, "Samsung Laptop", 66000, ProductCategory.Electronics),
                new Product(++this._idCounter, "HP Laptop", 55000, ProductCategory.Electronics),

                new Product(++this._idCounter, "Samsung Earphones", 499, ProductCategory.Electronics),
                new Product(++this._idCounter, "Nothing Earphones", 501, ProductCategory.Electronics),
                new Product(++this._idCounter, "Sony Earphones", 999, ProductCategory.Electronics),
                new Product(++this._idCounter, "Apple Earphones", 801, ProductCategory.Electronics),
                new Product(++this._idCounter, "Realme Earphones", 1099, ProductCategory.Electronics),

                new Product(++this._idCounter, "Atomic Habits", 599, ProductCategory.Book),
                new Product(++this._idCounter, "Power of Mind", 799, ProductCategory.Book),
                new Product(++this._idCounter, "Rich Dad Poor Dad", 200, ProductCategory.Book),
                new Product(++this._idCounter, "The Atlas", 199, ProductCategory.Book),
                new Product(++this._idCounter, "Harry Potter", 1239, ProductCategory.Book),
                new Product(++this._idCounter, "Lord of the Rings", 899, ProductCategory.Book),
            };
        }

        /// <summary>
        /// Retrieves all products in the database.
        /// </summary>
        /// <returns>A list of products.</returns>
        public List<Product> GetAllProducts()
        {
            return this._products;
        }
    }
}
