using Assignment9LINQAdvanced.Repository;
using Assignment9LINQAdvanced.Models.Enums;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents the Task 1 of the application.
    /// </summary>
    internal class Task1
    {
        private readonly ProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task1"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository instance used to query product.</param>
        public Task1(ProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        /// <summary>
        /// Runs the Task 1 of the application.
        /// </summary>
        internal void RunTask1()
        {
            var selectedProducts = this._productRepository.GetAllProducts()
                .Where(p => p.Category == ProductCategory.Electronics && p.Price > 500)
                .Select(p => new { p.Name, p.Price })
                .OrderByDescending(p => p.Price)
                .ToList();

            decimal average = selectedProducts.Average(p => p.Price);

            Console.WriteLine("Task 1\n");
            ConsoleTable
                .From(selectedProducts)
                .Configure(options => options.EnableCount = false)
                .Write();

            Console.WriteLine($"\nAVERAGE PRICE = {average:C}");
        }
    }
}
