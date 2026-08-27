using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment9LINQAdvanced.Database;
using Assignment9LINQAdvanced.Models;
using Assignment9LINQAdvanced.Models.Enums;
using ConsoleTables;

namespace Assignment9LINQAdvanced.Tasks
{
    internal class Task4
    {
        private readonly ProductDatabase _productDatabase;

        public Task4(ProductDatabase productDatabase)
        {
            this._productDatabase = productDatabase;
        }

        internal void RunTask4()
        {
            List<Product> products = this._productDatabase.GetAllProducts();

            var result = products
                .Where(product => product.Category == ProductCategory.Book)
                .OrderBy(product => product.Price)
                .ToList();

            ConsoleTable
                .From(result)
                .Configure(options => options.EnableCount = false)
                .Write();
        }
    }
}
