namespace Assignment3InventoryManagement.Services
{
    using Assignment3InventoryManagement.Models;
    using Assignment3InventoryManagement.Models.Enum;
    using Assignment3InventoryManagement.Persistence;
    using Assignment3InventoryManagement.Validation;
    using Spectre.Console;

    /// <summary>
    /// Represents a manager for handling product-related operations in the inventory management system.
    /// </summary>
    internal class InventoryService : IInventoryService
    {
        private readonly IRepository _repository;
        private readonly IProductValidation _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        /// <param name="repository">The repository to use for product operations.</param>
        /// <param name="validator">The validator to use for product validation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repository or validator is null.</exception>
        public InventoryService(IRepository repository, IProductValidation validator)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Creates a new product with the specified identifier, name, price, and quantity.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="quantity">Quantity of the product in stock.</param>
        /// <returns>A new Product instance with the provided details.</returns>
        public Product CreateProduct(string name, decimal price, int quantity)
        {
            return new Product
            {
                Id = this._repository.GetNextId(),
                Name = name,
                Price = price,
                Quantity = quantity,
            };
        }

        /// <summary>
        /// Creates a new Product with existing ID
        /// </summary>
        /// <param name="id">Existing product Id</param>
        /// <param name="name">Updated Name</param>
        /// <param name="price">Updated price</param>
        /// <param name="quantity">Updated quantity</param>
        /// <returns>New product object with updated details.</returns>
        public Product CreateUpdatedProduct(int id, string name, decimal price, int quantity)
        {
            return new Product
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
            };
        }

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        public string? AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            string validationError = this.ValidateProduct(product);

            if (string.IsNullOrEmpty(validationError))
            {
                this._repository.Add(product);
            }

            return validationError;
        }

        /// <summary>
        /// Retrieves the products list from the repository.
        /// </summary>
        /// <returns>A list of all products in repository.</returns>
        public List<Product> GetAllProducts()
        {
            List<Product> products = this._repository.GetAll().ToList();
            return products;
        }

        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        /// <param name="product">The product to remove.</param>
        /// <returns>A string containing the validation error message, or an empty string if the product is valid.</returns>
        public string? RemoveProduct(Product? product)
        {
            ArgumentNullException.ThrowIfNull(product);

            bool removed = this._repository.Remove(product.Id);

            if (!removed)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves the product based on the Id provided.
        /// </summary>
        /// <param name="id">Id of the existing product.</param>
        /// <returns>The matched product object, else null.</returns>
        public Product? GetProductById(int id)
        {
            return this._repository.GetById(id);
        }

        /// <summary>
        /// Updates the details of an existing product in the inventory.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <exception cref="ArgumentNullException">Thrown when the product is null.</exception>
        /// <returns>A string which represents the error occured, null if no error occured.</returns>
        public string UpdateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            string validationError = this.ValidateProduct(product);

            if (string.IsNullOrEmpty(validationError))
            {
                this._repository.Update(product);
            }

            return validationError;
        }

        /// <summary>
        /// Validates the specified product using the provided validator.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <returns>The validation error message, or an empty string if the product is valid.</returns>
        public string ValidateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            return this._validator.ValidateProduct(product) ?? string.Empty;
        }

        /// <summary>
        /// Searches for products whose properties match the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for within product properties.</param>
        /// <returns>A list of products that match the keyword.</returns>
        public List<Product> SearchProducts(string keyword)
        {
            ArgumentNullException.ThrowIfNull(keyword);

            List<Product> results = new List<Product>();
            List<Product> products = this._repository.GetAll().ToList();

            foreach (Product product in products)
            {
                if (this.IsMatch(product, keyword))
                {
                    results.Add(product);
                }
            }

            return results;
        }

        /// <inheritdoc />
        public bool CheckExistence(int id)
        {
            List<Product> products = this._repository.GetAll().ToList();

            foreach (Product product in products)
            {
                if (id == product.Id)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public string? AddStock(int id, int quantity)
        {
            Product? product = this.GetProductById(id);

            if (product is null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            product.Quantity += quantity;
            string errorMessage = this.ValidateProduct(product);
            if (string.IsNullOrEmpty(errorMessage))
            {
                this.UpdateProduct(product);
            }

            return errorMessage;
        }

        /// <inheritdoc />
        public string? RemoveStock(int id, int quantity)
        {
            Product? product = this.GetProductById(id);

            if (product is null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Quantity to remove cannot be negative.");
            }

            if (product.Quantity < quantity)
            {
                throw new ArgumentException("Insufficient stock. Net product quantity cannot be negative.");
            }

            product.Quantity -= quantity;
            string errorMessage = this.ValidateProduct(product);
            if (string.IsNullOrEmpty(errorMessage))
            {
                this.UpdateProduct(product);
            }

            return errorMessage;
        }

        /// <inheritdoc />
        public List<Product> GetSortedProducts(SortField sortField, bool isAscending)
        {
            List<Product> products = this.GetAllProducts();

            products.Sort((productX, productY) =>
            {
                if (productX == null && productY == null)
                {
                    return 0;
                }

                if (productX == null)
                {
                    return isAscending ? -1 : 1;
                }

                if (productY == null)
                {
                    return isAscending ? 1 : -1;
                }

                int comparisonResult = 0;

                switch (sortField)
                {
                    case SortField.Id:
                        comparisonResult = productX.Id.CompareTo(productY.Id);
                        break;

                    case SortField.Name:
                        comparisonResult = string.Compare(productX.Name, productY.Name, StringComparison.OrdinalIgnoreCase);
                        break;

                    case SortField.Price:
                        comparisonResult = productX.Price.CompareTo(productY.Price);
                        break;

                    case SortField.Quantity:
                        comparisonResult = productX.Quantity.CompareTo(productY.Quantity);
                        break;

                    default:
                        comparisonResult = productX.Id.CompareTo(productY.Id);
                        break;
                }

                return isAscending ? comparisonResult : -comparisonResult;
            });

            return products;
        }

        /// <summary>
        /// Determines whether a product has matches with the provided keyword.
        /// </summary>
        /// <param name="product">The product to evaluate.</param>
        /// <param name="keyword">The search keyword.</param>
        /// <returns>True if any field contains the search keyword; otherwise, false.</returns>
        private bool IsMatch(Product product, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return false;
            }

            bool idMatches = product.Id.ToString().Equals(keyword, StringComparison.OrdinalIgnoreCase);
            bool quantityMatches = product.Quantity.ToString().Equals(keyword, StringComparison.OrdinalIgnoreCase);
            bool priceMatches = product.Price.ToString("G").Equals(keyword, StringComparison.OrdinalIgnoreCase) ||
                           product.Price.ToString("F2").Equals(keyword, StringComparison.OrdinalIgnoreCase);
            bool nameMatches = product.Name != null && product.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase);

            return idMatches || nameMatches || priceMatches || quantityMatches;
        }
    }
}
