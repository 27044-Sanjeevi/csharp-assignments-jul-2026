using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;
using Assignment9LINQAdvanced.Repository;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents task 2 of the application.
    /// </summary>
    internal class Task2
    {
        private readonly ProductRepository _productRepository = new ProductRepository();
        private readonly SupplierRepository _supplierRepository = new SupplierRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="Task2"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository instance used to query products.</param>
        /// <param name="supplierRepository">The supplier repository instance used to query suppliers.</param>
        public Task2(ProductRepository productRepository, SupplierRepository supplierRepository)
        {
            this._productRepository = productRepository;
            this._supplierRepository = supplierRepository;
        }

        /// <summary>
        /// Runs the task 2 of the application.
        /// </summary>
        internal void RunTask2()
        {
            List<Product> products = this._productRepository.GetAllProducts();
            List<Supplier> suppliers = this._supplierRepository.GetAllSuppliers();
            IEnumerable<IGrouping<ProductCategory, Product>> groupByQuery = products
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

            IEnumerable<(ProductCategory Key, int Count, decimal ExpensiveProductPrice, string ExpensiveProductName)> projectionQuery = groupByQuery
                .Select(group => (
                    group.Key,
                    group.Count(),
                    group.Max(p => p.Price),
                    group.OrderByDescending(p => p.Price).First().Name));

            Console.WriteLine("Expensive Item in each group:\n");
            var projectionTable = new ConsoleTable("Category", "Count", "Expensive Product Price", "Expensive Product Name");
            foreach (var projection in projectionQuery)
            {
                projectionTable.AddRow(projection.Key, projection.Count, projection.ExpensiveProductPrice, projection.ExpensiveProductName);
            }

            projectionTable.Configure(options => options.EnableCount = false).Write();

            IEnumerable<(int ProductId, string ProductName, string SupplierName, int SupplierId)> joinQuery = suppliers
                .Join(
                    products,
                    supplier => supplier.ProductId,
                    product => product.Id,
                    (supplier, product) => (
                        product.Id,
                        product.Name,
                        supplier.SupplierName,
                        supplier.SupplierId));

            Console.WriteLine("\nJoined Table (products and suppliers):\n");
            var joinTable = new ConsoleTable("Product Id", "Product Name", "Supplier Name", "Supplier Id");
            foreach (var join in joinQuery)
            {
                joinTable.AddRow(join.ProductId, join.ProductName, join.SupplierName, join.SupplierId);
            }

            joinTable.Configure(options => options.EnableCount = false).Write();
        }
    }
}
