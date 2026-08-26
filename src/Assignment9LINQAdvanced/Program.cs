using Assignment9LINQAdvanced;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ProductManager.RunTask1();

            Console.ReadKey();
        }
    }
}