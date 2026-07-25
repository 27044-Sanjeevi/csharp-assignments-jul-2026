namespace Assignment2BasicsOfOOPs.Controller
{
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;
    using Assignment2BasicsOfOOPs.Services;
    using Assignment2BasicsOfOOPs.View;

    /// <summary>
    /// Controls the interaction between the Contol layer and View layer for the Employee payroll.
    /// </summary>
    internal class EmployeeController
    {
        private readonly ConsoleView _view;
        private readonly EmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="view">The console view renderer</param>
        /// <param name="employeeService">The service for the employee task</param>
        public EmployeeController(ConsoleView view, EmployeeService employeeService)
        {
            this._view = view;
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Prompts the user and runs Task 2 (Employee Hierarchy).
        /// </summary>
        public void RunEmployeeTask()
        {
            this._view.PrintHeader("Task 2: Employee Hierarchy");

            // 1. Manager Details
            this._view.PrintSubHeader("Manager Creation");
            string managerName = this._view.ReadString("Enter Manager Name: ");
            decimal managerSalary = this._view.ReadDecimal("Enter Manager Monthly Salary: Rs. ");
            Employee manager = this._employeeService.CreateEmployee(EmployeeRole.Manager, managerName, managerSalary);

            // 2. Developer Details
            this._view.PrintDivider();
            this._view.PrintSubHeader("Developer Creation");
            string devName = this._view.ReadString("Enter Developer Name: ");
            decimal devSalary = this._view.ReadDecimal("Enter Developer Monthly Salary: Rs. ");
            Employee developer = this._employeeService.CreateEmployee(EmployeeRole.Developer, devName, devSalary);

            // 3. Print Details
            this._view.PrintDivider();
            this._view.PrintSubHeader("Employee Payroll Details");
            this._view.PrintEmployeeDetails(manager);
            this._view.PrintEmployeeDetails(developer);
        }
    }
}
