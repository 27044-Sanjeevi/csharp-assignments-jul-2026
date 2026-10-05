using UtilityApp;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int leftOperand = ConsoleHelpers.ReadInteger("Enter first number: ");
            int rightOperand = ConsoleHelpers.ReadInteger("Enter second number: ");
            char operatorSymbol = ConsoleHelpers.ReadChar("Enter operator (+, -, *, /): ");
            double result;

            switch (operatorSymbol)
            {
                case '+':
                    result = leftOperand + rightOperand;
                    break;
                case '-':
                    result = leftOperand - rightOperand;
                    break;
                case '*':
                    result = leftOperand * rightOperand;
                    break;
                case '/':
                    if (rightOperand == 0)
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                        return;
                    }

                    result = leftOperand / rightOperand;
                    break;
                default:
                    throw new InvalidOperationException("Invalid operator. Please use +, -, *, or /.");
            }

            Console.WriteLine($"Result: {leftOperand} {operatorSymbol} {rightOperand} = {result}");
            Console.ReadKey();
        }
    }
}