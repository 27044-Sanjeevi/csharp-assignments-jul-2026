using Assignment10UnderstandingDotnet.Models;
using Assignment10UnderstandingDotnet.Models.Enums;
using Assignment10UnderstandingDotnet.Utilities;

namespace Assignment10UnderstandingDotnet.Services
{
    /// <summary>
    /// Provides business logic for mathematical operations.
    /// </summary>
    internal class MathOperationService
    {
        /// <summary>
        /// Calculates the result of a mathematical operation.
        /// </summary>
        /// <param name="operation">The mathematical operation to calculate.</param>
        /// <returns>The result of the mathematical operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when an unsupported operator is specified.</exception>
        public double Calculate(MathOperation operation)
        {
            ArgumentNullException.ThrowIfNull(operation);
            return operation.Operator switch
            {
                MathOperator.Addition =>
                    MathUtils.Add(operation.FirstNumber, operation.SecondNumber),
                MathOperator.Subtraction =>
                    MathUtils.Subtract(operation.FirstNumber, operation.SecondNumber),
                MathOperator.Multiplication =>
                    MathUtils.Multiply(operation.FirstNumber, operation.SecondNumber),
                MathOperator.Division =>
                    MathUtils.Divide(operation.FirstNumber, operation.SecondNumber),
                _ => throw new InvalidOperationException("Invalid operator."),
            };
        }
    }
}