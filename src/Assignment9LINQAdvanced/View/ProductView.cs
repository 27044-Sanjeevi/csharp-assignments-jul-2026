using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment9LINQAdvanced.Models;

namespace Assignment9LINQAdvanced.View
{
    internal class ProductView
    {
        public void DisplayProducts(IQueryable<Product> query)
        {
            foreach (var product in query)
            {

            }
        }
    }
}
