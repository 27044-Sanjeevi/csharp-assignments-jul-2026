namespace BMathApp
{
    /// <summary>
    /// Contains utility methods to perform basic mathematical operations.
    /// </summary>
    public static class MathOperations
    {
        /// <summary>
        /// Adds two integers together and returns the sum.
        /// </summary>
        /// <param name="leftOperand">The first integer to add.</param>
        /// <param name="rightOperand">The second integer to add.</param>
        /// <returns>The sum of the left and right operands.</returns>
        public static int Add(int leftOperand, int rightOperand)
        {
            return leftOperand + rightOperand;
        }

        /// <summary>
        /// Subtracts the right integer from the left integer.
        /// </summary>
        /// <param name="leftOperand">The base integer value.</param>
        /// <param name="rightOperand">The value to subtract from the base integer.</param>
        /// <returns>The difference after subtraction.</returns>
        public static int Subtract(int leftOperand, int rightOperand)
        {
            return leftOperand - rightOperand;
        }

        /// <summary>
        /// Multiplies two integers together.
        /// </summary>
        /// <param name="leftOperand">The first integer factor.</param>
        /// <param name="rightOperand">The second integer factor.</param>
        /// <returns>The product of the multiplication.</returns>
        public static int Multiply(int leftOperand, int rightOperand)
        {
            return leftOperand * rightOperand;
        }

        /// <summary>
        /// Divides the left integer by the right integer and returns a precise double.
        /// </summary>
        /// <param name="leftOperand">The dividend value.</param>
        /// <param name="rightOperand">The divisor value (must not be zero).</param>
        /// <returns>The floating-point quotient resulting from the division.</returns>
        /// <exception cref="DivideByZeroException">Thrown when the right operand is 0.</exception>
        public static double Divide(int leftOperand, int rightOperand)
        {
            if (rightOperand == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return (double)leftOperand / rightOperand;
        }
    }
}
