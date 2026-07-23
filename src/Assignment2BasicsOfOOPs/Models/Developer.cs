namespace Assignment2BasicsOfOOPs.Models
{
    using System;

    /// <summary>
    /// Represents a developer in an organization, derived from the Employee class.
    /// </summary>
    internal class Developer : Employee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Developer"/> class.
        /// </summary>
        /// <param name="name">Name of the Developer.</param>
        /// <param name="salary">Salary of the Developer.</param>
        public Developer(string name, decimal salary)
            : base(name, salary)
        {
        }

        /// <summary>
        /// Calculates the bonus of the Developer.
        /// </summary>
        /// <returns>The bonus in decimal type.</returns>
        public override decimal CalculateBonus() => 0.15M * this.Salary;

        /// <summary>
        /// Returns the formatted details of the Developer.
        /// </summary>
        /// <returns>A formatted string of details.</returns>
        public override string GetDetails() => $"Name: {this.Name}\n" +
                                               $"Position: {nameof(Developer)}\n" +
                                               $"Salary: Rs. {this.Salary:F2}\n" +
                                               $"Bonus: Rs. {this.CalculateBonus():F2}\n" +
                                               $"-----------------------\n";
    }
}
