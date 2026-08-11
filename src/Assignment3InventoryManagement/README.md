# Inventory Management
 
Inventory Management console application helps you manage product details and stock levels. You can add products, view the full inventory, search for products, update product details, delete products, and adjust stock quantities.
 
## Features
 
- Add a new product with a name, price, and initial quantity.
- View all products in a formatted table.
- Search products by ID, name, price, or quantity.
- Update the name and price of an existing product.
- Delete a product from the inventory.
- Add stock to an existing product.
- Remove stock from an existing product.
- Sort products by ID, name, price, or quantity in ascending or descending order.
 
## Main Menu
 
When the application starts, you the following options can be seen:
 
### Add a Product
 
1. Select option 1.
2. Enter the product name.
3. Enter the product price.
4. Enter the initial quantity.
 
The product ID is assigned automatically when the product is successfully added.
 
### View Products
 
1. Select option 2.
2. The application displays all products in a table.
 
The table shows:
 
- Product ID
- Product name
- Price
- Stock quantity
 
### Search Products
 
1. Select option 3.
2. Enter a search keyword.
 
The keyword can match:
 
- Product ID
- Product name
- Product price
- Product quantity
 
Matching products are displayed in a table.
 
### Update a Product
 
1. Select option 4.
2. Enter the ID of the product you want to update.
3. Enter a new name or leave it blank to keep the existing name.
4. Enter a new price or leave it blank to keep the existing price.
 
The application displays the old and new product details after a successful update.
 
### Delete a Product
 
1. Select option 5.
2. Enter the ID of the product you want to delete.
3. The product is removed from the inventory.
 
### Add Stock
 
1. Select option 6.
2. Enter the product ID.
3. Enter the quantity to add.
 
The updated product details are displayed after the stock is added.
 
### Remove Stock
 
1. Select option 7.
2. Enter the product ID.
3. Enter the quantity to remove.
 
The updated product details are displayed after the stock is removed.
 
### Sort Products
 
1. Select option 8.
2. Choose a field to sort by:
   - ID
   - Name
   - Price
   - Quantity
3. Choose a sort direction:
   - Ascending
   - Descending
 
The sorted products are displayed in a table.
 
## Input Rules
 
### Product Name
 
- Must not be empty.
- Must contain between 3 and 50 characters.
 
### Product Price
 
- Must be greater than 0.
 
### Product Quantity
 
- Must be 0 or greater.
- Stock removal cannot make the quantity negative.
 
### Product ID
 
- Must be a valid existing product ID for update, delete, add stock, and remove stock operations.
 
## Error Messages
 
The application displays clear messages when:
 
- A required value is missing.
- A product name is too short or too long.
- A product price is invalid.
- A product quantity is invalid.
- A product ID does not exist.
- Stock removal exceeds the available quantity.
 
## Navigation Tips
 
- Use the number keys or arrow-key selection menus where available.
- Press any key when prompted to return to the main menu.
- Leave optional update fields blank to keep the existing value.