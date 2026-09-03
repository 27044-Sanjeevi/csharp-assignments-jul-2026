using Assignment10UnderstandingDotnet.Models.Enums;

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
    }
}
