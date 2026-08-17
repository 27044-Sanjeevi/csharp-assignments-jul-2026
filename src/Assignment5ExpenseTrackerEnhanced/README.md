# Expense Tracker System

A console-based finance application for tracking income and expenses and generating financial reports. 

## Features

### Transaction Management
- Add new transactions with amount, Transaction type (Income/Expense), payment method, category, and optional description
- View all recorded transactions in a formatted dashboard table
- Update existing transaction details with partial update support (leave fields blank to retain current values)
- Delete transaction records permanently

### Filtering and Search
- Filter transactions by transaction type (Income or Expense)
- Filter transactions by category (Salary, Investment, Transport, Utilities, Groceries, Rent, Food, Shopping, etc.)

### Financial Reporting
- Generate financial insights including total income, total expenses, net balance, and transaction count
- Color-coded display distinguishing income (green) from expenses (red)
- Visualize the expenses made on different categories as chart.

### User Experience
- Interactive arrow-key menu navigation using Spectre.Console
- Input validation with error messages for invalid entries
- Partial update workflow allowing selective field modification
- Structured table rendering with aligned columns and borders

## Usage Guide

### Main Menu
Upon launch, the application displays an interactive menu. Use the Up and Down arrow keys to highlight an option, then press Enter to select it.

Available operations:
1. Add Transaction
2. View Transactions
3. Update Transaction
4. Delete Transaction
5. Filter Transactions
6. Sort Transactions
7. Search Transactions
8. Display Report
9. Exit

### Adding a Transaction
1. Select "Add Transaction" from the main menu.
2. Enter the transaction amount (must be greater than zero).
3. Select the transaction type: Income or Expense.
4. Select the payment method: Cash, Credit Card, Debit Card, or Bank Transfer.
5. Select the appropriate category based on the chosen Transaction type.
6. Optionally enter a description (press Enter to skip).
7. The system validates all inputs and either saves the transaction or displays validation errors.

### Viewing Transactions
Select "View Transactions" to display all recorded transactions in a formatted table showing ID, date/time, type, category, payment method, amount, and description.

### Updating a Transaction
1. Select "Update Transaction" from the main menu.
2. A table of all transactions is displayed. Enter the row number of the transaction to update.
3. For each field, enter a new value or press Enter to keep the current value.
4. The system validates the updated data and applies changes if valid.

### Deleting a Transaction
1. Select "Delete Transaction" from the main menu.
2. A table of all transactions is displayed. Enter the row number of the transaction to delete.
3. The transaction is permanently removed from the system.

### Filtering Transactions
1. Select "Filter Transactions" from the main menu.
2. Choose the filter parameter: Transaction Type or Category.
3. If filtering by Transaction Type, select Income or Expense.
4. If filtering by Category, first select the Transaction type, then select the specific category.
5. Matching transactions are displayed in a filtered table.

### Sort Transactions
1. Select "Sort Transactions" from the main menu.
2. Select your preferred sorting criteria: Date, Amount, Type, or Category.
3. Choose the sort order direction: Ascending or Descending.
4. The system fetches the sorted results into structured table. 
5. If no transactions exist, a "not found" notification will appear instead.

### Search Transactions
1. Select "Search Transactions" from the main menu.
2. Enter a search keyword or phrase to match against transaction descriptions.
3. The system scans the records and displays all matching transactions within a formatted results table.
4. If no records match the keyword, a descriptive message is shown

### Display Report
1. Select "Display Report" from the main menu to view your financial summary.
2. The dashboard will immediately display a summary card containing key statistics:
	1. Transaction Count: The total number of logs recorded.
	2. Total Income & Total Expenses: Raw financial turnarounds.
	3. Net Balance: Color-coded calculations showing profit (green) or net loss (red).
3. Also visual Spectre.Console elements will render:
	1. A Cash Flow Breakdown Chart tracking your dynamic Income-to-Expense ratio.
	2. A Bar Chart organizing and displaying your expenses ranked by category from highest to lowest.

## Architecture Overview

The application follows a layered architecture:

- **ApplicationRunner**: Owns the main execution loop and global exception handling
- **FinanceController**: Orchestrates user interactions between the View and Service layers
- **TransactionService**: Contains business logic, validation coordination, and report generation
- **TransactionValidation**: Performs structural validation of transaction data
- **CsvFileRepository**: Handles data persistence with defensive copying for state isolation
- **ConsoleView**: Manages all presentation rendering and user input collection
- **ConsoleHelper**: Provides reusable input parsing and Spectre.Console formatting utilities
- **ConsoleIO**: Abstracts raw console operations behind an interface

All layers communicate through interfaces, enabling testability and loose coupling. Data flows between layers via DTOs (TransactionInputDto, TransactionUpdateDto, ReportDto) to prevent direct model mutation across boundaries.

## Error Handling

- Validation failures are returned as structured ValidationResult objects containing specific error messages
- Expected exceptions (ArgumentException, KeyNotFoundException) are caught at the ApplicationRunner level and displayed as user-friendly messages
- Unexpected system errors are caught by a global safety net to prevent unhandled crashes