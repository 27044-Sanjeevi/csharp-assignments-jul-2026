# Expense Tracker System

A console-based finance application for tracking income and expenses and generating financial reports. 

## Features

### Transaction Management
- Add new transactions with amount, flow type (Income/Expense), payment method, category, and optional description
- View all recorded transactions in a formatted dashboard table
- Update existing transaction details with partial update support (leave fields blank to retain current values)
- Delete transaction records permanently

### Filtering and Search
- Filter transactions by flow type (Income or Expense)
- Filter transactions by category (Salary, Investment, Transport, Utilities, Groceries, Rent, Food, Shopping, etc.)

### Financial Reporting
- Generate financial insights including total income, total expenses, net balance, and transaction count
- Color-coded display distinguishing income (green) from expenses (red)

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
6. Generate Report
7. Exit

### Adding a Transaction
1. Select "Add Transaction" from the main menu.
2. Enter the transaction amount (must be greater than zero).
3. Select the flow type: Income or Expense.
4. Select the payment method: Cash, Credit Card, Debit Card, or Bank Transfer.
5. Select the appropriate category based on the chosen flow type.
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
2. Choose the filter parameter: Flow Type or Category.
3. If filtering by Flow Type, select Income or Expense.
4. If filtering by Category, first select the flow type, then select the specific category.
5. Matching transactions are displayed in a filtered table.

### Generating Reports
Select "Generate Report" to view a summary panel displaying:
- Total number of transactions
- Total income
- Total expenses
- Net balance (positive values shown in green, negative in red)

## Architecture Overview

The application follows a layered architecture:

- **ApplicationRunner**: Owns the main execution loop and global exception handling
- **FinanceController**: Orchestrates user interactions between the View and Service layers
- **TransactionService**: Contains business logic, validation coordination, and report generation
- **TransactionValidation**: Performs structural validation of transaction data
- **InMemoryRepository**: Handles data persistence with defensive copying for state isolation
- **ConsoleView**: Manages all presentation rendering and user input collection
- **ConsoleHelper**: Provides reusable input parsing and Spectre.Console formatting utilities
- **ConsoleIO**: Abstracts raw console operations behind an interface

All layers communicate through interfaces, enabling testability and loose coupling. Data flows between layers via DTOs (TransactionInputDto, TransactionUpdateDto, ReportDto) to prevent direct model mutation across boundaries.

## Error Handling

- Validation failures are returned as structured ValidationResult objects containing specific error messages
- Expected exceptions (ArgumentException, KeyNotFoundException) are caught at the ApplicationRunner level and displayed as user-friendly messages
- Unexpected system errors are caught by a global safety net to prevent unhandled crashes