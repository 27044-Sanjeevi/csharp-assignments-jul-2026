namespace Assignment2BasicsOfOOPs.Models
{
    /// <summary>
    /// Represents a manager in the organization, derived from Employee class.
    /// </summary>
    internal class Manager : Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="name">Name of the Manager.</param>
        /// <param name="salary">Salary of the Manager.</param>
        public Manager(string name, decimal salary)
            : base(name, salary)
        {
        }

        /// <summary>
        /// Calculates the bonus of the Manager.
        /// </summary>
        /// <returns>The bonus in decimal type.</returns>
        public override decimal CalculateBonus() => 0.25M * this.Salary;

        /// <summary>
        /// Returns the formatted details of the Manager.
        /// </summary>
        /// <returns>A formatted string of details.</returns>
        public override string GetDetails() => $"Name: {this.Name}\n" +
                                               $"Position: {nameof(Manager)}\n" +
                                               $"Salary: Rs. {this.Salary:F2}\n" +
                                               $"Bonus: Rs. {this.CalculateBonus():F2}\n" +
                                               $"-----------------------\n";
    }
}
