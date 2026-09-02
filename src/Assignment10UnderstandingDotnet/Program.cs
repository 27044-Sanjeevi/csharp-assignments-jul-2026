using Assignment10UnderstandingDotnet.Models;
using Assignment10UnderstandingDotnet.Utilities;
using Assignment10UnderstandingDotnet.View;

namespace Assignments
{
    /// <summary>
    /// Contains the Main entry point of the project.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Initializes dependencies and runs the controller.
        /// </summary>
        internal static void Main()
        {
            try
            {
                ConsoleView consoleView = new ();
                consoleView.DisplayApplicationTitle();
                MathOperation operation = new MathOperation()
                {
                    FirstNumber = consoleView.GetFirstNumber(),
                    SecondNumber = consoleView.GetSecondNumber(),
                    Operator = consoleView.GetOperator(),
                };

                double result = operation.Calculate();
                consoleView.DisplayResult(operation, result);
            }
            catch (DivideByZeroException ex)
            {
                ConsoleHelpers.DisplayException(ex.Message);
            }

            Console.WriteLine("\nPress any key to exit the application..");
            Console.ReadKey();
        }
    }
}