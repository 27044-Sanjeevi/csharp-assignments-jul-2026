using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models.Enums;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents the Task 1 of the application.
    /// </summary>
    internal class Task1
    {
        private readonly ProductDatabase _productDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task1"/> class.
        /// </summary>
        /// <param name="productDatabase">The product database instance used to query product.</param>
        public Task1(ProductDatabase productDatabase)
        {
            this._productDatabase = productDatabase;
        }

        /// <summary>
        /// Runs the Task 1 of the application.
        /// </summary>
        internal void RunTask1()
        {
            ProductDatabase database = new ProductDatabase();

            var selectedProducts = database.GetAllProducts()
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
