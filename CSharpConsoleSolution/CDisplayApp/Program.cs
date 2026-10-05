using System.ComponentModel;
using BMathApp;
using DUtilityApp;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PerformCalculation();
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayFailure($"An unexpected error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void PerformCalculation()
        {
            int leftOperand = ConsoleHelpers.ReadInteger("Enter first number: ");
            int rightOperand = ConsoleHelpers.ReadInteger("Enter second number: ");
            char operatorSymbol = ConsoleHelpers.ReadChar("Enter operator (+, -, *, /): ");
            double result = 0;
            try
            {
                result = operatorSymbol switch
                {
                    '+' => MathOperations.Add(leftOperand, rightOperand),
                    '-' => MathOperations.Subtract(leftOperand, rightOperand),
                    '*' => MathOperations.Multiply(leftOperand, rightOperand),
                    '/' => MathOperations.Divide(leftOperand, rightOperand),
                    _ => throw new InvalidOperationException("Invalid operator. Please use +, -, *, or /."),
                };
            }
            catch (DivideByZeroException ex)
            {
                ConsoleHelpers.DisplayFailure(ex.Message);
            }

            Console.WriteLine($"Result: {leftOperand} {operatorSymbol} {rightOperand} = {result:F2}");
        }
    }
}