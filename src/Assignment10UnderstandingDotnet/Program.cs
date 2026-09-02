using Assignment10UnderstandingDotnet;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                View view = new ();

                MathOperation operation = new MathOperation()
                {
                    FirstNumber = view.GetFirstNumber(),
                    SecondNumber = view.GetSecondNumber(),
                    Operator = view.GetOperator(),
                };

                int result = operation.Calculate();
                view.DisplayResult(operation);
            }
            catch (DivideByZeroException ex)
            {
                ConsoleHelpers.DisplayException(ex.Message);
            }

            Console.ReadKey();
        }
    }
}