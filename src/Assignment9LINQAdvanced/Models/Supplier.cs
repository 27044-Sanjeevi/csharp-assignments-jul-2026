using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9LINQAdvanced.Models
{
    /// <summary>
    /// Represents a supplier.
    /// </summary>
    internal class Supplier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Supplier"/> class.
        /// </summary>
        /// <param name="id">The unique supplier ID.</param>
        /// <param name="supplierName">The name of the supplier.</param>
        /// <param name="productId">The unique product ID.</param>
        public Supplier(int id, string supplierName, int productId)
        {
            this.SupplierId = id;
            this.SupplierName = supplierName;
            this.ProductId = productId;
        }

        /// <summary>
        /// Gets the supplier ID.
        /// </summary>
        /// <value>An integer holding the unique ID of the supplier.</value>
        public int SupplierId { get; }

        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        /// <value>A string holding the name of the supplier.</value>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        /// <value>An integer holding a unique product ID.</value>
        public int ProductId { get; set; }
    }
}
