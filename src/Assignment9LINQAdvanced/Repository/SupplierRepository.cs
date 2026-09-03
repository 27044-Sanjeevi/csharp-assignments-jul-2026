using Assignment9LINQAdvanced.Models;

namespace Assignment9LINQAdvanced.Repository
{
    /// <summary>
    /// Represents the supplier repository.
    /// </summary>
    internal class SupplierRepository
    {
        private const int _stationerySupplierId = 1;
        private const int _cookwareSupplierId = 2;
        private const int _electronicsSupplierId = 3;
        private const int _booksSupplierId = 4;

        private readonly List<Supplier> _suppliers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierRepository"/> class.
        /// </summary>
        public SupplierRepository()
        {
            this._suppliers = new List<Supplier>(this.LoadData());
        }

        // public IQueryable<Supplier> GetProducts() => this._suppliers.AsQueryable();

        /// <summary>
        /// Retrieves all suppliers in the repository.
        /// </summary>
        /// <returns>A list of suppliers.</returns>
        public List<Supplier> GetAllSuppliers()
        {
            return this._suppliers;
        }

        private List<Supplier> LoadData()
        {
            var suppliers = new List<Supplier>();

            // 6 Stationery products (IDs 1 to 6)
            for (int i = 1; i <= 6; i++)
            {
                suppliers.Add(new Supplier(_stationerySupplierId, "Stationery Supplier", i));
            }

            // 5 Cookware products (IDs 7 to 11)
            for (int i = 7; i <= 11; i++)
            {
                suppliers.Add(new Supplier(_cookwareSupplierId, "Cookware Supplier", i));
            }

            // 17 Electronics products (IDs 12 to 29)
            for (int i = 12; i <= 29; i++)
            {
                suppliers.Add(new Supplier(_electronicsSupplierId, "Electronics Supplier", i));
            }

            // 6 Books products (IDs 29 to 35)
            for (int i = 30; i <= 35; i++)
            {
                suppliers.Add(new Supplier(_booksSupplierId, "Books Supplier", i));
            }

            return suppliers;
        }
    }
}
