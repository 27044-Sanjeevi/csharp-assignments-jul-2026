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
