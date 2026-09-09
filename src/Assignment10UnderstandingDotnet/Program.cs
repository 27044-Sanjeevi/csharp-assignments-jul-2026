using Assignment10UnderstandingDotnet.Models;
using Assignment10UnderstandingDotnet.Services;
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
            const string FirstNumberPrompt = "Enter the first number (integer): ";
            const string SecondNumberPrompt = "Enter the second number (integer): ";

            try
            {
                ConsoleView consoleView = new ();
                MathOperationService mathOperationService = new ();

                consoleView.DisplayApplicationTitle();

                MathOperation operation = new MathOperation()
                {
                    FirstNumber = consoleView.GetInteger(FirstNumberPrompt),
                    SecondNumber = consoleView.GetInteger(SecondNumberPrompt),
                    Operator = consoleView.GetOperator(),
                };

                double result = mathOperationService.Calculate(operation);
                consoleView.DisplayResult(operation, result);
            }
            catch (ArgumentNullException ex)
            {
                ConsoleHelpers.DisplayException("[ARGUMENT NULL EXCEPTION]: " + ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                ConsoleHelpers.DisplayException("[DIVISION BY ZERO EXCEPTION]: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ConsoleHelpers.DisplayException("[INVALID OPERATION EXCEPTION]: " + ex.Message);
            }
            catch (Exception ex)
            {
                ConsoleHelpers.DisplayException(ex.Message);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit the application..");
                Console.ReadKey();
            }
        }
    }
}