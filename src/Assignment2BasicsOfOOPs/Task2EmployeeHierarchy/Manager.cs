namespace Assignment2BasicsOfOOPs.Task2EmployeeHierarchy
{
    /// <summary>
    /// Represents a manager in the organization, derived from Employee class
    /// </summary>
    internal class Manager : Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="name">name of the Manager</param>
        /// <param name="salary">Salary of the Manager</param>
        public Manager(string name, decimal salary)
            : base(name, salary)
        {
            // empty body, the parent constructor handles the assignment.
        }

        /// <summary>
        /// calculates the bonus of the Manager
        /// </summary>
        /// <returns>The bonus in decimal type</returns>
        public override decimal CalculateBonus() => 0.25M * this.Salary;

        /// <summary>
        /// Prints the details of the Manager
        /// </summary>
        public override void PrintDetails()
        {
            ConsoleIO.Write($"Name: {this.Name}\n" +
                            $"Position: {nameof(Manager)}\n" +
                            $"Salary: Rs. {this.Salary}\n" +
                            $"Bonus: Rs. {this.CalculateBonus()}\n" +
                            $"-----------------------\n");
        }
    }
}
