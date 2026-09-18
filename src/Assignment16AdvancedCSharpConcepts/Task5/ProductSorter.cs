using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Task5
{
    internal class ProductSorter
    {
        public delegate int SortDelegate(Product product1, Product product2);

        public void SortAndDisplay(List<Product> products, SortDelegate sortDelegate)
        {
            List<Product> clonedProducts = new List<Product>(products);
            clonedProducts.Sort(new Comparison<Product>(sortDelegate));
            ConsoleHelpers.WriteColored($"{"Name",-20} | {"Category",-12} | {"Price",7}\n", ConsoleColor.Cyan);
            Console.WriteLine(new string('-', 50));
            foreach (var product in clonedProducts)
            {
                Console.WriteLine($"{product.Name,-20} | {product.Category,12} | {product.Price,7}");
            }
        }
    }
}
