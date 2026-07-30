namespace Assignment2BasicsOfOOPs.Services
{
    using System;
    using Assignment2BasicsOfOOPs.Models;
    using Assignment2BasicsOfOOPs.Models.Enums;
    using Assignment2BasicsOfOOPs.Validation;

    /// <summary>
    /// Provides services for employee-related operations.
    /// </summary>
    internal class EmployeeService
    {
        private readonly EmployeeValidation _employeeValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="employeeValidator">Provides the validation for the employee.</param>
        public EmployeeService(EmployeeValidation employeeValidator)
        {
            this._employeeValidator = employeeValidator;
        }

        /// <summary>
        /// Creates a new employee with the specified role, name, and salary.
        /// </summary>
        /// <param name="role">The role assigned to the employee.</param>
        /// <param name="name">The name of the employee.</param>
        /// <param name="salary">The salary of the employee.</param>
        /// <returns>The newly created Employee instance.</returns>
        public Employee CreateEmployee(EmployeeRole role, string name, decimal salary)
        {
            ArgumentNullException.ThrowIfNull("The name cannot be null.", nameof(name));

            if (!this._employeeValidator.IsValidSalary(salary))
            {
                throw new ArgumentException("Salary cannot be negative.");
            }

            return role switch
            {
                EmployeeRole.Manager => new Manager(name, salary),
                EmployeeRole.Developer => new Developer(name, salary),
                _ => throw new ArgumentException($"Unknown Employee Role : {role}", nameof(role))
            };
        }

        /// <summary>
        /// Calculates the bonus for a given employee.
        /// </summary>
        /// <param name="employee">The employee object.</param>
        /// <returns>The calculated bonus, or 0M if employee is null.</returns>
        public decimal GetBonus(Employee employee)
        {
            ArgumentNullException.ThrowIfNull(employee);

            return employee.CalculateBonus();
        }
    }
}