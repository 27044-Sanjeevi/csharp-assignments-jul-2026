using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment10UnderstandingDotnet
{
    internal class View
    {
        public int GetFirstNumber()
        {
            return ConsoleHelpers.ReadInt("Enter first number (integer): ");
        }

        public int GetSecondNumber()
        {
            return ConsoleHelpers.ReadInt("Enter first number (integer): ");
        }

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

        public void DisplayResult(MathOperation operation)
        {
            string operatorSymbol = operation.Operator switch
            {
                MathOperator.Addition => "+",
                MathOperator.Subtraction => "-",
                MathOperator.Multiplication => "*",
                MathOperator.Division => "/",
                _ => throw new InvalidOperationException("Invalid operator"),
            };

            ConsoleHelpers.WriteColored($"\nResult: {operation.FirstNumber} {operatorSymbol} {operation.SecondNumber} = {operation.Calculate()}", ConsoleColor.Green);
        }
    }
}
