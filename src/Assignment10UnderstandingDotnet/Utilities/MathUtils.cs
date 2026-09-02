namespace Assignment10UnderstandingDotnet.Utilities
{
    /// <summary>
    /// Provides the methods for performing mathematical operations.
    /// </summary>
    internal static class MathUtils
    {
        /// <summary>
        /// Calculates the addition of two integers.
        /// </summary>
        /// <param name="firstNumber">The first integer for addition.</param>
        /// <param name="secondNumber">The second integer for addition.</param>
        /// <returns>An integer holding the added value of first and second numbers.</returns>
        public static int Add(int firstNumber, int secondNumber) => firstNumber + secondNumber;

        /// <summary>
        /// Calculates the subtraction of two integers.
        /// </summary>
        /// <param name="firstNumber">The first integer for subtraction.</param>
        /// <param name="secondNumber">The second integer for subtraction.</param>
        /// <returns>An integer holding the subtracted value of first and second numbers.</returns>
        public static int Subtract(int firstNumber, int secondNumber) => firstNumber - secondNumber;

        /// <summary>
        /// Calculates the mulitplication of two integers.
        /// </summary>
        /// <param name="firstNumber">The first integer for mulitplication.</param>
        /// <param name="secondNumber">The second integer for mulitplication.</param>
        /// <returns>An integer holding the mulitplied value of first and second numbers.</returns>
        public static int Multiply(int firstNumber, int secondNumber) => firstNumber * secondNumber;

        /// <summary>
        /// Calculates the division of two integers.
        /// </summary>
        /// <param name="firstNumber">The first integer for division.</param>
        /// <param name="secondNumber">The second integer for division.</param>
        /// <returns>A double holding the divided value of first and second numbers.</returns>
        public static double Divide(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException("The divisor should not be 0 for division.");
            }

            return (double) firstNumber / secondNumber;
        }
    }
}
