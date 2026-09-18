using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Task5
{
    internal class SortStrategy
    {
        public int SortByName(Product product1, Product product2)
        {
            return product1.Name.CompareTo(product2.Name);
        }

        public int SortByCategory(Product product1, Product product2)
        {
            return product1.Category.CompareTo(product2.Category);
        }

        public int SortByPrice(Product product1, Product product2)
        {
            return product1.Price.CompareTo(product2.Price);
        }
    }
}
