namespace Assignment10UnderstandingDotnet
{
    internal static class MathUtils
    {
        public static int Add(int firstNumber, int secondNumber) => firstNumber + secondNumber;

        public static int Subtract(int firstNumber, int secondNumber) => firstNumber - secondNumber;

        public static int Multiply(int firstNumber, int secondNumber) => firstNumber * secondNumber;

        public static int Divide(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException("The divisor should not be 0 for division.");
            }

            return firstNumber / secondNumber;
        }
    }
}
