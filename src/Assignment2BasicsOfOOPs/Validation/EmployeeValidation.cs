namespace Assignment2BasicsOfOOPs.Validation
{
    /// <summary>
    /// Represents the validations related to the employee.
    /// </summary>
    internal class EmployeeValidation
    {
        /// <summary>
        /// Validates the name of the employee.
        /// </summary>
        /// <param name="name">The name to be validated.</param>
        /// <returns>true if name is meaningful, otherwise false.</returns>
        public bool IsValidName(string name) => !string.IsNullOrWhiteSpace(name);

        /// <summary>
        /// Validates the salary.
        /// </summary>
        /// <param name="salary">The salary to be validated.</param>
        /// <returns>true if salary is not negative, else false.</returns>
        public bool IsValidSalary(decimal salary) => salary >= 0;
    }
}
