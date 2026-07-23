namespace Assignment2BasicsOfOOPs.Services
{
    using System;
    using Assignment2BasicsOfOOPs.Models;

    /// <summary>
    /// Provides services for employee-related operations.
    /// </summary>
    internal class EmployeeService
    {
        /// <summary>
        /// Calculates the bonus for a given employee.
        /// </summary>
        /// <param name="employee">The employee object.</param>
        /// <returns>The calculated bonus, or 0M if employee is null.</returns>
        public decimal GetBonus(Employee employee)
        {
            if (employee == null)
            {
                return 0M;
            }

            return employee.CalculateBonus();
        }
    }
}