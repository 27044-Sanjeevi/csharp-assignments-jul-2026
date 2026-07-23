namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Represents the base class for the employee hierarchy in an organization.
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="name">Name of the Employee.</param>
        /// <param name="salary">Salary of the Employee.</param>
        public Employee(string? name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Gets or sets the Name of the Employee.
        /// </summary>
        /// <value>A string holding the name of the employee.</value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Salary of the Employee.
        /// </summary>
        /// <value>A decimal holding the Salary of the employee.</value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Provides the abstract method to calculate the Bonus for the employee.
        /// </summary>
        /// <returns>The bonus in decimal type.</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        /// Provides the abstract method to return the details of the Employee as a string.
        /// </summary>
        /// <returns>A formatted string of employee details.</returns>
        public abstract string GetDetails();
    }
}
