using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models;
using ConsoleTables;

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
        /// <param name="productDatabase">The product database instance used to query products.</param>
        /// <param name="supplierDatabase">The supplier database instance used to query suppliers.</param>
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
            Console.WriteLine("Grouped Products:\n");
            var table = new ConsoleTable("Id", "Name", "Price", "Category");
            foreach (var group in groupByQuery)
            {
                foreach (var product in group)
                {
                    table.AddRow(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Category);
                }
            }

            table
                .Configure(options => options.EnableCount = false)
                .Write();

            var projectionQuery = groupByQuery
                .Select(group => new
                {
                    CategoryName = group.Key,
                    ProductCount = group.Count(),
                    ExpensiveItemPrice = group.Max(p => p.Price),
                    ExpensiveItemName = group.OrderByDescending(p => p.Price).First().Name,
                });

            Console.WriteLine("Expensive Item in each group:\n");
            ConsoleTable
                .From(projectionQuery)
                .Configure(options => options.EnableCount = false)
                .Write();

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

            Console.WriteLine("\nJoined Table (products and suppliers):\n");
            ConsoleTable
                .From(joinQuery)
                .Configure(options => options.EnableCount = false)
                .Write();
        }
    }
}
