# LINQ CHALLENGES

## Task 1: Basic LINQ Queries 

Given a List<Product>, where Product is a class with properties ProductId, ProductName, Price, and Category, write LINQ queries to:  

- Filter products under the category "Electronics" with a price greater than $500 and select only ProductName and Price. 

- Using the result of the previous query, sort these filtered products in descending order of price. 

- Find the average price of these filtered products. 

## Task 2: Complex LINQ Queries 

With the same List<Product>	 

- Group products by category and count the products in each category. Each group should also have the most expensive product in that category 

- Perform an inner join with a List<Supplier>, where Supplier is a class with properties SupplierId, SupplierName, and ProductId, to match products with their suppliers. 

## Task 3: LINQ to Objects 

Given an array of integers, use LINQ to find the: 

- Second highest number in the array. 

- All unique pairs of numbers in the array that add up to a specified target. 

## Task 4: Performance Considerations with LINQ 

Given a List<Product>, write two LINQ queries: 

- One that selects all products under the category "Books" and sorts them by price. 

- An optimized version of the above query.

## Task 5: Query Builder 
Created a query builder utility that allows users to construct complex LINQ queries using a fluent API pattern. This utility supports filtering, sorting and joining data. 