using Assignment10UnderstandingDotnet.Models;
using Assignment10UnderstandingDotnet.Models.Enums;
using Assignment10UnderstandingDotnet.Utilities;

namespace Assignment10UnderstandingDotnet.View
{
    /// <summary>
    /// Provides the methods for interacting with the user through console.
    /// </summary>
    internal class ConsoleView
    {
        /// <summary>
        /// Reads the first number for a mathematical operation.
        /// </summary>
        /// <returns>The retrieved integer from the user.</returns>
        public int GetFirstNumber()
        {
            return ConsoleHelpers.ReadInt("Enter first number (integer): ");
        }

        /// <summary>
        /// Reads the second number for a mathematical operation.
        /// </summary>
        /// <returns>The retrieved integer from the user.</returns>
        public int GetSecondNumber()
        {
            return ConsoleHelpers.ReadInt("Enter second number (integer): ");
        }

        /// <summary>
        /// Reads the operator for mathematical operation from the user.
        /// </summary>
        /// <returns>An enum specifying the operator for the mathematical operation.</returns>
        public MathOperator GetOperator()
        {
            while (true)
            {
                Console.Write("\nEnter operator (+, -, *, /): ");
                char selectedOperator = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (selectedOperator)
                {
                    case '+': return MathOperator.Addition;
                    case '-': return MathOperator.Subtraction;
                    case '*': return MathOperator.Multiplication;
                    case '/': return MathOperator.Division;

                    default:
                        ConsoleHelpers.WriteColored("Invalid operator. Please enter one of the following: +, -, *, /.", ConsoleColor.Red);
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the result of the given mathematical operation.
        /// </summary>
        /// <param name="operation">The instance holding a mathematical operation.</param>
        /// <param name="result">The result of the operation.</param>
        /// <exception cref="InvalidOperationException">Thrown when an invalid operator is encountered.</exception>
        public void DisplayResult(MathOperation operation, double result)
        {
            string operatorSymbol = operation.Operator switch
            {
                MathOperator.Addition => "+",
                MathOperator.Subtraction => "-",
                MathOperator.Multiplication => "*",
                MathOperator.Division => "/",
                _ => throw new InvalidOperationException("Invalid operator"),
            };

            string numberFormat = (operatorSymbol == "/") ? "F4" : "F0";

            ConsoleHelpers.WriteColored(
                $"\nResult: {operation.FirstNumber} {operatorSymbol} {operation.SecondNumber} = {result.ToString(numberFormat)}",
                ConsoleColor.Yellow);
        }

        /// <summary>
        /// Displays the title of the application.
        /// </summary>
        public void DisplayApplicationTitle()
        {
            ConsoleHelpers.WriteColored("--- CALCULATOR ---\n", ConsoleColor.Cyan);
        }
    }
}
