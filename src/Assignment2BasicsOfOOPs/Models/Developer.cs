namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Represents a developer in an organization, derived from the Employee class
    /// </summary>
    internal class Developer : Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Developer"/> class.
        /// </summary>
        /// <param name="name">name of the Developer</param>
        /// <param name="salary">Salary of the Developer</param>
        public Developer(string name, decimal salary)
            : base(name, salary)
        {
            // empty body, the parent constructor handles the assignment.
        }

        /// <summary>
        /// calculates the bonus of the Manager
        /// </summary>
        /// <returns>The bonus in decimal type</returns>
        public override decimal CalculateBonus() => 0.15M * this.Salary;

        /// <summary>
        /// Prints the details of the Manager
        /// </summary>
        /// <returns>The string containing the details of the Employee</returns>
        public override string PrintDetails() => $"\nName: {this.Name}\n" +
                            $"Position: {nameof(Developer)}\n" +
                            $"Salary: Rs. {this.Salary}\n" +
                            $"Bonus: Rs. {this.CalculateBonus()}\n" +
                            $"-----------------------\n";
    }
}
