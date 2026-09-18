using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Task5
{
    internal class Product
    {
        public Product(string name, ProductCategory category, decimal price)
        {
            this.Name = name;
            this.Category = category;
            this.Price = price;
        }

        public string Name { get; set; }

        public ProductCategory Category { get; set; }

        public decimal Price { get; set; }
    }
}
