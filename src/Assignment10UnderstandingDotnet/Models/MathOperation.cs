using Assignment10UnderstandingDotnet.Models.Enums;
using Assignment10UnderstandingDotnet.Utilities;

namespace Assignment10UnderstandingDotnet.Models
{
    /// <summary>
    /// Represents a mathematical operation with two operands and an operator.
    /// </summary>
    internal class MathOperation
    {
        /// <summary>
        /// Gets or sets the first number for the mathematical operation.
        /// </summary>
        /// <value>An integer holding the first number.</value>
        public int FirstNumber { get; set; }

        /// <summary>
        /// Gets or sets the second number of the mathematical operation.
        /// </summary>
        /// <value>An integer holding the second number.</value>
        public int SecondNumber { get; set; }

        /// <summary>
        /// Gets or sets the operator for the mathematical operation.
        /// </summary>
        /// <value>An enum which represents the operator involved in the mathematical operation.</value>
        public MathOperator Operator { get; set; }

        /// <summary>
        /// Calculates the result of the mathematical operation based on the specified operator and operands.
        /// </summary>
        /// <returns>An integer holding the resultant value of the mathematical operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when invalid operator is chosen.</exception>
        public double Calculate()
        {
            return this.Operator switch
            {
                MathOperator.Addition =>
                    MathUtils.Add(this.FirstNumber, this.SecondNumber),
                MathOperator.Subtraction =>
                    MathUtils.Subtract(this.FirstNumber, this.SecondNumber),
                MathOperator.Multiplication =>
                    MathUtils.Multiply(this.FirstNumber, this.SecondNumber),
                MathOperator.Division =>
                    MathUtils.Divide(this.FirstNumber, this.SecondNumber),
                _ =>
                    throw new InvalidOperationException("Invalid operator"),
            };
        }
    }
}
