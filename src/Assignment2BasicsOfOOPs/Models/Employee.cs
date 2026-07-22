namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Represent the base class for the employee heirarchy in an organization
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="name">Name of the Employee</param>
        /// <param name="salary">Salary of the Employee</param>
        public Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Gets or sets the Name of the Employee
        /// </summary>
        /// <value>A string holding the name of the employee</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Salary of the Employee
        /// </summary>
        /// <value>A decimal holdoing the Salary of the employee</value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Provides the abstract method to calculate the Bonus for the employee
        /// </summary>
        /// <returns>The bonus in decimal type</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        ///  Provides the abstract method to print the details of the Employee
        /// </summary>
        /// <returns>A string containing the details of the Emplpoyee.</returns>
        public abstract string PrintDetails();
    }
}
