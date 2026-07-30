# Assignment 2: Basics of OOPs
 
This console application includes fundamental Object-Oriented Programming (OOP) concepts including **Inheritance**, **Polymorphism**, and **Encapsulation**. It features three distinct modules: a Shape Calculator, an Employee Payroll System, and a Bank Account Manager.
 
## Features
 
### 1. Shape Hierarchy
*   Calculate the area of different geometric shapes.
*   Supports **Rectangles** and **Circles**.
*   Includes input validation to ensure colors contain only alphabetic characters.
 
### 2. Employee Payroll System
*   Manage different types of employees with specific bonus structures.
*   **Managers:** Receive a 25% bonus on their monthly salary.
*   **Developers:** Receive a 15% bonus on their monthly salary.
*   Displays detailed payroll information including name, position, salary, and calculated bonus.
 
### 3. Bank System
*   Simulate real-world banking transactions.
*   **Savings Account:** Does not allow overdrafts; withdrawals are blocked if funds are insufficient.
*   **Checking Account:** Allows overdrafts for flexible spending.
*   Perform deposits, withdrawals, and view account balances in real-time.

## Usage Guide
 
Once the application starts, you will see the **Main Menu**. Use the number keys to navigate:
 
### Main Menu
*   **1:** Task 1 - Shape Hierarchy
*   **2:** Task 2 - Employee Hierarchy
*   **3:** Task 3 - Bank System
*   **4:** Exit Application
 
### Task 1: Shapes
1.  Enter the color for your Rectangle (letters only).
2.  Enter the height and width.
3.  Enter the color for your Circle.
4.  Enter the radius.
5.  The app will display the calculated areas for both.
 
### Task 2: Employees
1.  Enter the Name and Monthly Salary for a **Manager**.
2.  Enter the Name and Monthly Salary for a **Developer**.
3.  The app will generate a payroll report showing their total earnings including bonuses.
 
### Task 3: Banking
1.  Choose between a **Savings** or **Checking** account.
2.  Enter an Account Number and Initial Deposit.
3.  Use the action menu to:
    *   **Deposit** money.
    *   **Withdraw** money (try withdrawing more than your balance in a Savings account to see the validation logic!).
    *   **Print Details** to see your current status.
 
## Project Structure

1. **Controller**: Handles user interaction flow and navigation.
2. **Models**: Contains the core data structures (Shapes, Employees, Accounts).

3. **Services**: Implements business logic and calculations.

4. **Validation**: Ensures input data meets required formats and rules.

5. **View**: Manages all console output, formatting, and user prompts.

 - All inputs are validated to prevent crashes.
- The application uses a modular design, making it easy to add new shapes, employee roles, or account types in the future.
