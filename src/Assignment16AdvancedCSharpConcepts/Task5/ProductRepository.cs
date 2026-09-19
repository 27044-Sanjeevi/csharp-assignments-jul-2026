namespace Assignment16AdvancedCSharpConcepts.Task5
{
    /// <summary>
    /// Represents the product repository.
    /// </summary>
    internal class ProductRepository
    {
        private readonly List<Product> _products = new List<Product>()
        {
            new Product("Apple Laptop", ProductCategory.Electronics, 95_000m),
            new Product("Xiaomi Laptop", ProductCategory.Electronics, 55_9990m),
            new Product("Samsung Laptop", ProductCategory.Electronics, 85_000m),
            new Product("Huawei Laptop", ProductCategory.Electronics, 35_000m),
            new Product("Lenovo Laptop", ProductCategory.Electronics, 72_000m),

            new Product("Pen", ProductCategory.Stationery, 25m),
            new Product("Pencil", ProductCategory.Stationery, 10m),
            new Product("Eraser", ProductCategory.Stationery, 4m),
            new Product("Sharpener", ProductCategory.Stationery, 7m),
            new Product("Crayons", ProductCategory.Stationery, 50m),

            new Product("Lays", ProductCategory.Snack, 5m),
            new Product("Biscuit", ProductCategory.Snack, 10m),
            new Product("KitKat", ProductCategory.Snack, 15m),
            new Product("Snickers", ProductCategory.Snack, 20m),
            new Product("Murukku", ProductCategory.Snack, 30m),
        };

        /// <summary>
        /// Retrieves all the products in the repository.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{T}"/> containing the list of products.</returns>
        public IReadOnlyList<Product> GetProducts() => this._products;
    }
}
