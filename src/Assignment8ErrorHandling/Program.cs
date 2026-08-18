using System.Runtime.CompilerServices;

namespace Assignments
{
    /// <summary>
    /// Provides methods for performing division operations and printing exception details.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry
        /// </summary>
        public static void Main()
        {
            try
            {
                double res = Divide(10, 0);
                Console.WriteLine(res);
            }
            catch (DivideByZeroException ex)
            {
                PrintException(ex);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Divides the given two numbers.
        /// </summary>
        /// <param name="a">Dividend value</param>
        /// <param name="b">Divisor Value</param>
        /// <returns>Divided Value</returns>
        public static double Divide(int a, int b)
        {
                return a / b;
        }

        /// <summary>
        /// Prints exception
        /// </summary>
        /// <param name="ex">Exception object</param>
        public static void PrintException(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine(ex.Source);
            Console.WriteLine(ex.InnerException);
            Console.WriteLine(ex.TargetSite);
        }
    }
}